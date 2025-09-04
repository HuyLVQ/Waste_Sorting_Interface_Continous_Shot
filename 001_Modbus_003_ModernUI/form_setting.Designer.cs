namespace _001_Modbus_003_ModernUI
{
    partial class form_setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connect_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.baud_comboBox = new System.Windows.Forms.ComboBox();
            this.com_comboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.run_classifier_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.camera_connect_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.connect_button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.baud_comboBox);
            this.groupBox1.Controls.Add(this.com_comboBox);
            this.groupBox1.Font = new System.Drawing.Font("Nirmala UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 504);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modbus settings";
            // 
            // connect_button
            // 
            this.connect_button.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connect_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.connect_button.Location = new System.Drawing.Point(198, 468);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(70, 30);
            this.connect_button.TabIndex = 3;
            this.connect_button.Text = "Apply";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Select BaudRate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select COM";
            // 
            // baud_comboBox
            // 
            this.baud_comboBox.FormattingEnabled = true;
            this.baud_comboBox.Location = new System.Drawing.Point(6, 285);
            this.baud_comboBox.Name = "baud_comboBox";
            this.baud_comboBox.Size = new System.Drawing.Size(246, 33);
            this.baud_comboBox.TabIndex = 0;
            this.baud_comboBox.Click += new System.EventHandler(this.baud_comboBox_click);
            // 
            // com_comboBox
            // 
            this.com_comboBox.FormattingEnabled = true;
            this.com_comboBox.Location = new System.Drawing.Point(6, 75);
            this.com_comboBox.Name = "com_comboBox";
            this.com_comboBox.Size = new System.Drawing.Size(246, 33);
            this.com_comboBox.TabIndex = 0;
            this.com_comboBox.Click += new System.EventHandler(this.com_comboBox_click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.run_classifier_button);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Font = new System.Drawing.Font("Nirmala UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(296, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 259);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File and Image Brower";
            // 
            // run_classifier_button
            // 
            this.run_classifier_button.Enabled = false;
            this.run_classifier_button.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.run_classifier_button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.run_classifier_button.Location = new System.Drawing.Point(182, 223);
            this.run_classifier_button.Name = "run_classifier_button";
            this.run_classifier_button.Size = new System.Drawing.Size(70, 30);
            this.run_classifier_button.TabIndex = 4;
            this.run_classifier_button.Text = "Apply";
            this.run_classifier_button.UseVisualStyleBackColor = true;
            this.run_classifier_button.Click += new System.EventHandler(this.run_classifier_button_click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(6, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(246, 40);
            this.button2.TabIndex = 0;
            this.button2.Text = "Image Directory browser";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.open_image_folder_click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Script browser";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.script_browser_button_click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.camera_connect_button);
            this.groupBox3.Font = new System.Drawing.Font("Nirmala UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(296, 342);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 155);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Camera";
            // 
            // camera_connect_button
            // 
            this.camera_connect_button.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.camera_connect_button.Location = new System.Drawing.Point(6, 71);
            this.camera_connect_button.Name = "camera_connect_button";
            this.camera_connect_button.Size = new System.Drawing.Size(246, 40);
            this.camera_connect_button.TabIndex = 1;
            this.camera_connect_button.Text = "Connect to Camera";
            this.camera_connect_button.UseVisualStyleBackColor = true;
            this.camera_connect_button.Click += new System.EventHandler(this.camera_connect_button_click);
            // 
            // form_setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1183, 593);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_setting";
            this.Text = "form_setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox baud_comboBox;
        public System.Windows.Forms.ComboBox com_comboBox;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button connect_button;
        public System.Windows.Forms.Button run_classifier_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Button camera_connect_button;
    }
}