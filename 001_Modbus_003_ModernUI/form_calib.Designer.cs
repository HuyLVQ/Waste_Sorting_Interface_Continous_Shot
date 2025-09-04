namespace _001_Modbus_003_ModernUI
{
    partial class form_calib
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.picture_groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.execute_calib_button = new System.Windows.Forms.Button();
            this.calib_capture_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.log_box = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.picture_groupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 39);
            this.label1.TabIndex = 9;
            this.label1.Text = "Calibration";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::_001_Modbus_003_ModernUI.Properties.Resources.default_image;
            this.pictureBox.Location = new System.Drawing.Point(45, 38);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 500);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // picture_groupBox
            // 
            this.picture_groupBox.Controls.Add(this.pictureBox);
            this.picture_groupBox.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picture_groupBox.Location = new System.Drawing.Point(16, 60);
            this.picture_groupBox.Name = "picture_groupBox";
            this.picture_groupBox.Size = new System.Drawing.Size(879, 562);
            this.picture_groupBox.TabIndex = 10;
            this.picture_groupBox.TabStop = false;
            this.picture_groupBox.Text = "Calibration View";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.execute_calib_button);
            this.groupBox1.Controls.Add(this.calib_capture_button);
            this.groupBox1.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(902, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 179);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Executing";
            // 
            // execute_calib_button
            // 
            this.execute_calib_button.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.execute_calib_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.execute_calib_button.Location = new System.Drawing.Point(68, 106);
            this.execute_calib_button.Name = "execute_calib_button";
            this.execute_calib_button.Size = new System.Drawing.Size(160, 31);
            this.execute_calib_button.TabIndex = 13;
            this.execute_calib_button.Text = "Calibrate";
            this.execute_calib_button.UseVisualStyleBackColor = true;
            this.execute_calib_button.Click += new System.EventHandler(this.execute_calib_button_Click);
            // 
            // calib_capture_button
            // 
            this.calib_capture_button.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calib_capture_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.calib_capture_button.Location = new System.Drawing.Point(68, 38);
            this.calib_capture_button.Name = "calib_capture_button";
            this.calib_capture_button.Size = new System.Drawing.Size(160, 31);
            this.calib_capture_button.TabIndex = 12;
            this.calib_capture_button.Text = "Capture Image";
            this.calib_capture_button.UseVisualStyleBackColor = true;
            this.calib_capture_button.Click += new System.EventHandler(this.calib_capture_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.log_box);
            this.groupBox2.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(902, 245);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 377);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs";
            // 
            // log_box
            // 
            this.log_box.Location = new System.Drawing.Point(6, 34);
            this.log_box.Name = "log_box";
            this.log_box.Size = new System.Drawing.Size(275, 319);
            this.log_box.TabIndex = 0;
            this.log_box.Text = "";
            // 
            // form_calib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1201, 640);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picture_groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_calib";
            this.Text = "form_calib";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.picture_groupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox pictureBox;
        public System.Windows.Forms.GroupBox picture_groupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button execute_calib_button;
        public System.Windows.Forms.Button calib_capture_button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox log_box;
    }
}