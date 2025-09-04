using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace _001_Modbus_003_ModernUI
{
    public partial class form_calib : Form
    {
        public string calib_image_path = "";
        private string calib_image_file = "";
        private int calib_count = 20;

        private Form1 mainForm;
        public form_calib(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        /// <summary>
        /// This function is activated each time the Calib Capture Button is clicked
        /// First, it prompts the Camera to capture once.
        /// Then, it stores the newly captured image path and displays it.
        /// Finally, it cleans the placeholder image for the next capturing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calib_capture_button_Click(object sender, EventArgs e)
        {
            string message = this.mainForm.camera.camera_oneshot_capture(this.calib_image_path, calib_count);
            if (message == "Image is captured successfully")
            {
                this.calib_image_file = this.mainForm.image_path + "captured_image" + calib_count.ToString() + ".jpg";
                this.pictureBox.Image = Image.FromFile(this.calib_image_file);

                this.log_box.SelectionStart = this.log_box.TextLength;
                this.log_box.SelectionLength = 0;

                this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Bold);
                this.log_box.SelectionColor = Color.Red;
                this.log_box.AppendText(">>");

                this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Regular);
                this.log_box.SelectionColor = this.log_box.ForeColor;
                this.log_box.AppendText("Image Captured!\n");


                if (calib_count == 1)                                                
                {
                    calib_count = 0;
                }
                else
                {
                    calib_count++;
                }

                this.mainForm.TryDeleteFile(Path.Combine(this.mainForm.image_path, "captured_image" + calib_count.ToString() + ".jpg"));
            }
        }


        /// <summary>
        /// This function is activated each time the Execute Calib Button is clicked
        /// The function receives the message from calibration program and processed image file path
        /// Then, it proceeds to display the message to the message logs. If the calibration is succesful, 
        ///     it changes the is_finished_calib flag to signal other functionalities and activates the
        ///     backgroundWorker1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void execute_calib_button_Click(object sender, EventArgs e)
        {
            this.mainForm.start_python_script(this.mainForm.python_script_path);

            string retv_message = "", feedback_image_path = "";
            (retv_message, feedback_image_path) = this.mainForm.read_from_calibration(this.calib_image_file);

            this.pictureBox.Image = Image.FromFile(feedback_image_path);

            this.log_box.SelectionStart = this.log_box.TextLength;
            this.log_box.SelectionLength = 0;

            this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Bold);
            this.log_box.SelectionColor = Color.Red;
            this.log_box.AppendText(">>");

            this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Regular);
            this.log_box.SelectionColor = this.log_box.ForeColor;
            this.log_box.AppendText(retv_message + "\n");

            if (retv_message == "Successfully Calibrated")
            {
                this.log_box.SelectionStart = this.log_box.TextLength;
                this.log_box.SelectionLength = 0;

                this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Bold);
                this.log_box.SelectionColor = Color.Red;
                this.log_box.AppendText(">>");

                this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Regular);
                this.log_box.SelectionColor = this.log_box.ForeColor;
                this.log_box.AppendText("Waiting...\n");

                this.mainForm.is_finished_calib = true;
                this.mainForm.warm_up_read_from_python(ref this.mainForm.is_first_inference);

                this.mainForm.backgroundWorker1.RunWorkerAsync();
                this.mainForm.backgroundWorker2.RunWorkerAsync();
                this.mainForm.backgroundWorker3.RunWorkerAsync();

                this.mainForm.form_home_instance.stop_button.Enabled = true;
                this.mainForm.form_home_instance.stop_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                
                this.mainForm.form_home_instance.stop_classifier_button.Enabled = true;
                this.mainForm.form_home_instance.stop_classifier_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;

                this.log_box.SelectionStart = this.log_box.TextLength;
                this.log_box.SelectionLength = 0;

                this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Bold);
                this.log_box.SelectionColor = Color.Red;
                this.log_box.AppendText(">>");

                this.log_box.SelectionFont = new Font("Nirmala UI", 6, FontStyle.Regular);
                this.log_box.SelectionColor = this.log_box.ForeColor;
                this.log_box.AppendText("Calibration is Successful, Proceed\n");
            }
        }
    }
}
