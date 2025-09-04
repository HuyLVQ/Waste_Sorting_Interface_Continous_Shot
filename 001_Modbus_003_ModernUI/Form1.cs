using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;  
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;
using MaterialSkin.Controls;
using _001_Modbus_003_ModernUI.Properties;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Collections;


namespace _001_Modbus_003_ModernUI
{
    public partial class Form1 : Form
    {
        public ModbusClient modbus_client = null;                     // Modbus Client
        DataTable table = new DataTable("table");                     // DataTable for displaying the Predicted Encoder values + Bottles Classes

        public const int sensor_address = 0x0002;                     // Address of Sensor
        public const int enc_address = 0x000A;                        // Address of Encoder
        public const int line1_recv_addr = 0x00C8;                    // Address of Start of Receive Address of Line 1 
        public const int line2_recv_addr = 0x012C;                    // Address of Start of Receive Address of Line 2 
        public const int line3_recv_addr = 0x0190;                    // Address of Start of Receive Address of Line 3 
        public const int line4_recv_addr = 0x01F4;                    // Address of Start of Receive Address of Line 4
        public const int line5_recv_addr = 0x01F4;                    // Address of Start of Receive Address of Line 5

        public bool image_is_open = false;
        public bool script_is_open = false;

        private const int CAPTURE_INTERVAL = 500;
        private const int N1_OFFSET = 100;
        private const int N2_OFFSET = 200;
        private const int N3_OFFSET = 300;
        private const int N4_OFFSET = 400;
        public int[] offset_value = new int[] { N1_OFFSET, N2_OFFSET, N3_OFFSET, N4_OFFSET };      // Values of Ejectors offsets from Camera along the Conveyor Belt axis
                                                                                    
        public Queue<(int, string)> event_queue = new Queue<(int, string)>();                      // Queue holds the encoder value at the time capturing and captured image file path
        public const int event_queue_size = 10;                                                    // Maximum captured images stored in queue

        public Queue<(int, int)> line1_queue = new Queue<(int, int)>();                            // Queue holds the Predicted Bottles Classe + Predicted Encoder Value of the bottle within the frame

        public string python_path;                                                       // Path of Python Executor
        public string python_script_path;                                                // Path of Model's Python path
        public string image_path;                                                        // Path of Capured Image's path
        public DirectoryInfo directory;                                                     
        public bool is_first_inference = true;                                           // Denote the first inference occurence
        public bool is_finished_calib = false;                                           // Denote the calibration is finished

        public Process python_process;                                                   // Process of Python which is ran concurrently
        public StreamWriter python_stdin;
        public StreamReader python_stdout;

        private readonly object modbus_lock = new object();                              // The Mutex Lock of Modbus usage
        private readonly object event_queue_lock = new object();                         // The Mutex Lock of event queue
        private readonly object queue_lock = new object();                               // The Mutex Lock of event queue

        public static AutoResetEvent event_queue_is_empty = new AutoResetEvent(false);   // The Event signal corresponds to the transition from no element to one element in event_queue
        public static AutoResetEvent value_queue_is_empty = new AutoResetEvent(false);   // The Event signal corresponds to the transition from no element to one element in value_queue 
                                                                                         
        public BaslerCamera camera;
        public bool camera_is_open = false;
        public int count = 0;

        public form_setting form_setting_instance;
        public form_home form_home_instance;
        public form_calib form_calib_instance;

        private Stopwatch capture_stopwatch = new Stopwatch();


        /// <summary>
        /// Cleans up resources such as Python process, camera, Modbus client, and temporary image files.
        /// </summary>
        private void CleanupResources()
        {
            try
            {
                if (backgroundWorker1 != null && backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
                if (backgroundWorker2 != null && backgroundWorker2.IsBusy)
                {
                    backgroundWorker2.CancelAsync();
                }
                if (backgroundWorker3 != null && backgroundWorker3.IsBusy)
                {
                    backgroundWorker3.CancelAsync();
                }

                Thread.Sleep(200);

                backgroundWorker1?.Dispose();
                backgroundWorker2?.Dispose();
                backgroundWorker3?.Dispose();
            }
            catch (Exception ex)
            {
                this.Invoke((Action)(() =>
                {
                    MessageBox.Show(this, $"Failed to clean up background workers: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }));
            }


            try
            {
                // Stop and dispose Python process
                if (python_process != null && !python_process.HasExited)
                {
                    try
                    {
                        python_stdin?.Close();
                        python_stdout?.Close();
                        python_process.Kill();
                        python_process.WaitForExit(1000); // Wait up to 1 second for clean exit
                        python_process.Dispose();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show(this, $"Failed to terminate Python process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }));
                    }
                    finally
                    {
                        python_process = null;
                        python_stdin = null;
                        python_stdout = null;
                    }
                }

                // Dispose camera
                if (camera != null && camera_is_open)
                {
                    try
                    {
                        camera.camera_close(); // Assumes BaslerCamera has a Close method; adjust if different
                        camera_is_open = false;
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show(this, $"Failed to close camera: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }));
                    }
                    finally
                    {
                        camera = null;
                    }
                }

                // Disconnect Modbus client
                if (modbus_client != null && modbus_client.Connected)
                {
                    try
                    {
                        modbus_client.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show(this, $"Failed to disconnect Modbus client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }));
                    }
                    finally
                    {
                        modbus_client = null;
                    }
                }

                // Clean up temporary image files
                if (!string.IsNullOrEmpty(image_path) && Directory.Exists(image_path))
                {
                    try
                    {
                        foreach (var file in Directory.GetFiles(image_path, "captured_image.jpg"))
                        {
                            if (!TryDeleteFile(file, 5, 100))
                            {
                                this.Invoke((Action)(() =>
                                {
                                    MessageBox.Show(this, $"Failed to delete image file: {file}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show(this, $"Failed to clean up image files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke((Action)(() =>
                {
                    MessageBox.Show(this, $"Unexpected error during cleanup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }


        /// <summary>
        /// Helper Function for deleting a file through multiple attemps, each 1ms apart
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="maxRetries"></param>
        /// <param name="delayMs"></param>
        /// <returns></returns>
        public bool TryDeleteFile(string filePath, int maxRetries = 3, int delayMs = 1)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        return true;
                    }
                    return false;
                }
                catch (IOException)
                {
                    if (i == maxRetries - 1) return false; // Give up after max retries
                    Thread.Sleep(delayMs); // Wait before retrying
                }
            }
            return false;
        }


        /// <summary>
        /// This function initializes the Python Script once at the beginning.
        ///     It is activated whenever the Calibration process is completed
        /// </summary>
        /// <param name="_python_script_path"></param>
        public void start_python_script(string _python_script_path)
        {
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = python_path,
                Arguments = $"\"{_python_script_path}\"",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            python_process = new Process { StartInfo = psi };
            python_process.Start();

            python_stdin = python_process.StandardInput;
            python_stdout = python_process.StandardOutput;
        }


        /// <summary>
        /// This function reads results from AI model inference
        /// The output of the Model follows "Line_Index==||Bottle_ID1-Y_COM1||Bottle_ID2-Y_COM2||Bottle_ID3-Y_COM3"
        ///     The results is organized as follows:
        ///         - feedback_image_path:   Path to processed image            (string)
        ///         - predicted_enc:         Predicted encoder feedback value   (int)
        ///         - bottle_class:          Class of bottle                    (int)
        ///     If the queue is empty then the resulting message would be : "Line_Index==||". Thus, the retrieved queue is empty
        /// </summary>
        /// <param name="_image_path"></param>
        /// <returns></returns>
        ///
        public (string file_path, Queue<(int, int)>) read_from_python(string _image_path, ref bool _is_first_inference)
        {
            var queue = new Queue<(int bottle_id, int y_com)>();                            // Create the placeholders for the parsed values of incoming messages

            python_stdin.WriteLine("{0}", _image_path);                                     // Write to the captured image's file path to the IPC Stream
            python_stdin.Flush();

            int message_num = int.Parse(python_stdout.ReadLine());                          // Retrieve number of Valid Messages
            string[] message_sequence = new string[message_num];                            // Create the placeholders for the incoming messages

            for (int i = 0; i < message_num; i++)                                           // Store the incoming messages
            {
                message_sequence[i] = python_stdout.ReadLine();
            }

            // --- If the resulting queue is "Line_Index==||", then, the queue is empty --- //
            if (message_sequence[1].Length == 5)
            {
                return (message_sequence[0], queue);
            }

            ReadOnlySpan<char> span = message_sequence[1].AsSpan();                         // Convert to Span type

            int eqPos = span.IndexOf("==".AsSpan());                                        // Get the ID of "==" to retrieve LINE_ID
            int line_id = int.Parse(span.Slice(0, eqPos).ToString());

            int pos = eqPos + 4;                                                            // Skip past "==||"
             
            // --- Parse each bottle-yCom pair --- //
            while (pos < span.Length)
            {
                // --- Find next separator "||" or end --- //
                int nextSep = span.Slice(pos).IndexOf("||".AsSpan());                   // Find the next occurence of "||"
                if (nextSep == -1) nextSep = span.Length - pos;                         // If the next occrunce of || is -1, then, there are no more ||
                var pair = span.Slice(pos, nextSep);                                    //          Thus, indicating the last value pair

                if (pair.Length > 0)
                {
                    int dashPos = pair.IndexOf('-');                                    // Split the string into 2, which are seperated by '-' (get the first occurence only)
                    int bottle_id = int.Parse(pair.Slice(0, dashPos).ToString());       //          And, parse the string to obtain BOTTLE_ID and Y_COM
                    int y_com = int.Parse(pair.Slice(dashPos + 1).ToString());

                    queue.Enqueue((bottle_id, y_com));
                }

                pos += nextSep + 2;                                                     // Move past "||"       
            }
            return (message_sequence[0], queue);                                        // Return the processed image's file path and the processed queue
        }



        /// <summary>
        /// This function warms up the Inference by simply executes 5 capturing-and-processing.
        ///     .The outputs are ignored.
        ///     .This function only occured once after the calibration is completed.
        /// </summary>
        /// <param name="_is_first_inference"></param>
        public void warm_up_read_from_python(ref bool _is_first_inference)
        {
            int temp_idx = 100;

            for (int i = 0; i < 20; i++)
            {
                // --- Capture the camera --- //
                _ = camera.camera_oneshot_capture(this.image_path, temp_idx);
                string image_file = Path.Combine(this.image_path, "captured_image" + temp_idx.ToString() + ".jpg");

                python_stdin.WriteLine("{0}", image_file);
                python_stdin.Flush();

                // --- If this is the first inference, ignore the first 3 contents ---//
                if (_is_first_inference == false)
                {
                    ;
                }
                else
                {
                    for (int z = 0; z < 3; z++)
                    {
                        python_stdout.ReadLine();
                    }
                    _is_first_inference = false;
                }

                // --- Ignore --- //
                for (int j = 0; j < 3; j++)
                {
                    string temp_string = python_stdout.ReadLine();
                }
                temp_idx++;
            }
            return;
        }


        /// <summary>
        /// This function read from the feedback of Calibration function
        /// </summary>
        /// <param name="_image_path"></param>
        /// <returns></returns>
        public (string, string) read_from_calibration(string _image_path)
        {
            python_stdin.WriteLine("{0}", _image_path);                                     // Send to the terminal the Path of Captured Image        
            python_stdin.Flush();                                   

            string retv_message = python_stdout.ReadLine();
            string feedback_image_path = python_stdout.ReadLine();

            if (retv_message == "Successfully Calibrated")                                  // If the Calibration is successful, change the calib value                                      
            {                                                                               
                this.is_finished_calib = true;  
            }
            
            return (retv_message, feedback_image_path);                                     // Return the Calibration Message for Logging and Feedback Image (with CheckerBoard painted on)
        }


        public Form1()
        {
            InitializeComponent();

            this.home_form_load_panel.Controls.Clear();

            this.form_home_instance = new form_home(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.form_calib_instance = new form_calib(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.form_setting_instance = new form_setting(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

            this.form_home_instance.FormBorderStyle = FormBorderStyle.None;
            this.home_form_load_panel.Controls.Add(this.form_home_instance);
            this.form_home_instance.Show();
        }


        private void home_button_Click(object sender, EventArgs e)
        {
            navigation_panel.Height = home_button.Height;
            navigation_panel.Top = home_button.Top;
            navigation_panel.Left = home_button.Left;

            home_button.BackColor = ColorTranslator.FromHtml("#F8F4EC");
            home_button.ForeColor = ColorTranslator.FromHtml("#121212");

            calib_button.BackColor = ColorTranslator.FromHtml("#402b3a");
            calib_button.ForeColor = ColorTranslator.FromHtml("#F8F4EC");
            setting_button.BackColor = ColorTranslator.FromHtml("#402b3a");
            setting_button.ForeColor = ColorTranslator.FromHtml("#F8F4EC");

            this.home_form_load_panel.Controls.Clear();
            this.home_form_load_panel.Controls.Add(this.form_home_instance);
            this.form_home_instance.Show();
        }


        private void calib_button_Click(object sender, EventArgs e)
        {
            navigation_panel.Height = calib_button.Height;
            navigation_panel.Top = calib_button.Top;
            navigation_panel.Left = calib_button.Left;

            calib_button.BackColor = ColorTranslator.FromHtml("#F8F4EC");
            calib_button.ForeColor = ColorTranslator.FromHtml("#121212");

            home_button.BackColor = ColorTranslator.FromHtml("#402b3a");
            home_button.ForeColor = ColorTranslator.FromHtml("#F8F4EC");
            setting_button.BackColor = ColorTranslator.FromHtml("#402b3a");
            setting_button.ForeColor = ColorTranslator.FromHtml("#F8F4EC");

            this.home_form_load_panel.Controls.Clear();
            this.home_form_load_panel.Controls.Add(form_calib_instance);
            form_calib_instance.Show();
        }


        private void setting_button_Click(object sender, EventArgs e)
        {
            navigation_panel.Height = setting_button.Height;
            navigation_panel.Top = setting_button.Top;
            navigation_panel.Left = setting_button.Left;

            setting_button.BackColor = ColorTranslator.FromHtml("#F8F4EC");
            setting_button.ForeColor = ColorTranslator.FromHtml("#121212");

            home_button.BackColor = ColorTranslator.FromHtml("#402b3a");
            home_button.ForeColor = ColorTranslator.FromHtml("#F8F4EC");
            calib_button.BackColor = ColorTranslator.FromHtml("#402b3a");
            calib_button.ForeColor = ColorTranslator.FromHtml("#F8F4EC");

            this.home_form_load_panel.Controls.Clear();
            this.home_form_load_panel.Controls.Add(form_setting_instance);
            form_setting_instance.Show();
        }


        private void exit_button_Click(object sender, EventArgs e)
        {
            CleanupResources();
            Application.Exit();
        }


        /// <summary>
        /// This function activated by the time the Program starts.
        /// It also initializes a Table with 3 distincted Columns Line ID - Encoder Feedback - Predicted Class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void program_load(object sender, EventArgs e)
        {
            table.Columns.Add("Line", Type.GetType("System.Int32"));
            table.Columns.Add("Encoder Feedback", Type.GetType("System.Int32"));
            table.Columns.Add("Predicted Class", Type.GetType("System.String"));

            form_home_instance.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6, FontStyle.Bold);
            form_home_instance.dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 6, FontStyle.Regular);
            form_home_instance.dataGridView1.RowTemplate.Height = 25;

            form_home_instance.dataGridView1.DataSource = table;
        }



        /// <summary>
        /// This function populates available COM ports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void com_comboBox_click(object sender, EventArgs e)
        {
            form_setting_instance.com_comboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            form_setting_instance.com_comboBox.Items.AddRange(ports);
            if (form_setting_instance.com_comboBox.Items.Count > 1)
            {
                form_setting_instance.com_comboBox.SelectedItem = ports[0];
            }
        }


        /// <summary>
        /// This Thread is used to Continously Capture the image every CAPTURE_INTERVAL ms
        /// By the time the camera shots, the Thread reads the Encoder Value stored in PLC
        /// The Thread also ensures the Time between two consecutive shots' triggers is maintained at
        ///     CAPTURE_INTERLVAL ms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                capture_stopwatch.Restart();
                string message = camera.camera_oneshot_capture(this.image_path, this.count);
                if (message == "Image is captured successfully")
                {
                    string image_file = this.image_path + "captured_image" + this.count.ToString() + ".jpg";
                    int encoder_feedback = -1;
                    int[] temporary_storage = new int[2];

                    // --- Read and store the encoder read-out value --- //
                    lock (modbus_lock)
                    {
                        temporary_storage = modbus_client.ReadHoldingRegisters(enc_address, 2);
                        encoder_feedback = ((temporary_storage[0] & 0xFFFF) | (temporary_storage[1] << 16));
                    }

                    lock (event_queue_lock)
                    {
                        event_queue.Enqueue((encoder_feedback, image_file));
                    }

                    if (count == event_queue_size - 1)                                                // If the indexing reaches the limit, the index must return
                    {
                        count = 0;
                    }
                    else
                    {
                        count++;
                    }

                    if (event_queue.Count == 1)                                                        // If Event Queue is previously empty and the element size is now 1 
                    {                                                                                  //    ,then, trigger the BackgroundThread3 for executing Model Inference Mode
                        event_queue_is_empty.Set();
                    }
                    capture_stopwatch.Stop();


                    if (capture_stopwatch.ElapsedMilliseconds < CAPTURE_INTERVAL)
                    {
                        Thread.Sleep(CAPTURE_INTERVAL - (int)capture_stopwatch.ElapsedMilliseconds);
                    }
                     
                }
            }
        }


        /// <summary>
        /// This backgroundWorker is continously running to processed the received data and transmit
        ///     to the PLC via Modbus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker2.CancellationPending)
            {
                value_queue_is_empty.WaitOne();
                while (true)
                {
                    int a = -1;
                    int b = -1;

                    bool line1_finish = false;

                    lock (queue_lock)
                    {
                        if (line1_queue.Count > 0)
                        {
                            (a, b) = line1_queue.Dequeue();
                        }
                        else
                        {
                            line1_finish = true;
                        }
                    }

                    lock (modbus_lock)
                    {
                        if (a >= 2000000)
                        {
                            a -= 2000000;
                        }
                        try
                        {
                            modbus_client.WriteMultipleRegisters(line1_recv_addr, new int[] { 1, (a & 0xFFFF), (a >> 16) & 0xFFFF, b });
                        }
                        catch (Exception ex)
                        {
                            ;
                        }
                    }

                    // --- Break from continous processing and Wait for new Event to arrive --- //
                    if (line1_finish)
                    {
                        break;                                                      
                    }
                }
            }
        }


        /// <summary>
        /// This backgroundWorker runs concurrently to process the incoming data. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker3.CancellationPending)
            {
                event_queue_is_empty.WaitOne();
                while (true)
                {
                    string image_file = string.Empty;
                    int encoder_feedback = 0;

                    string feedback_image_path = string.Empty;
                    var result = new Queue<(int, int)>();

                    lock (event_queue_lock)
                    {
                        if (event_queue.Count == 0)
                        {
                            break;
                        }
                        (encoder_feedback, image_file) = event_queue.Dequeue();
                    }

                    (feedback_image_path, result) = read_from_python(image_file, ref is_first_inference);

                    this.Invoke((Action)(() =>
                    {
                        this.form_home_instance.pictureBox.Image = Image.FromFile(feedback_image_path);
                    }));

                    if (result.Count > 0)
                    {

                        foreach (var item in result)
                        {
                            if (item.Item1 != 10 && item.Item1 != -1)
                            {
                                lock (queue_lock)
                                {
                                    encoder_feedback += offset_value[item.Item1] + item.Item2;
                                    line1_queue.Enqueue((encoder_feedback, item.Item1));
                                }
                            }
                            else
                            {
                                encoder_feedback = -1;
                            }

                            this.Invoke((Action)(() =>
                            {
                                DataRow newRow = table.NewRow();
                                newRow["Line"] = 1;
                                newRow["Encoder Feedback"] = encoder_feedback;
                                newRow["Predicted Class"] = item.Item1;
                                table.Rows.InsertAt(newRow, 0);
                            }));

                        }
                        value_queue_is_empty.Set();
                    }
                    
                    if (!TryDeleteFile(image_file, 3, 1))
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show(this, "Could not Delete captured image ", "User Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }));
                    }
                }
            }
        }
    }
}
