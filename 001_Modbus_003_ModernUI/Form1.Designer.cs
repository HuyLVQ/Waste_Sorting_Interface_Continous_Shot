namespace _001_Modbus_003_ModernUI
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.calib_button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.exit_button = new System.Windows.Forms.Button();
            this.home_button = new System.Windows.Forms.Button();
            this.setting_button = new System.Windows.Forms.Button();
            this.home_form_load_panel = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.navigation_panel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(43)))), ((int)(((byte)(58)))));
            this.panel1.Controls.Add(this.navigation_panel);
            this.panel1.Controls.Add(this.calib_button);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.exit_button);
            this.panel1.Controls.Add(this.home_button);
            this.panel1.Controls.Add(this.setting_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 640);
            this.panel1.TabIndex = 0;
            // 
            // calib_button
            // 
            this.calib_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(43)))), ((int)(((byte)(58)))));
            this.calib_button.FlatAppearance.BorderSize = 0;
            this.calib_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calib_button.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calib_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.calib_button.Location = new System.Drawing.Point(3, 460);
            this.calib_button.Name = "calib_button";
            this.calib_button.Size = new System.Drawing.Size(186, 60);
            this.calib_button.TabIndex = 3;
            this.calib_button.Text = "Calibration";
            this.calib_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.calib_button.UseVisualStyleBackColor = false;
            this.calib_button.Click += new System.EventHandler(this.calib_button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::_001_Modbus_003_ModernUI.Properties.Resources.bk_logo_image1;
            this.pictureBox1.InitialImage = global::_001_Modbus_003_ModernUI.Properties.Resources.logo_image;
            this.pictureBox1.Location = new System.Drawing.Point(32, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 91);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // exit_button
            // 
            this.exit_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(43)))), ((int)(((byte)(58)))));
            this.exit_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.exit_button.FlatAppearance.BorderSize = 0;
            this.exit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit_button.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.exit_button.Location = new System.Drawing.Point(0, 580);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(186, 60);
            this.exit_button.TabIndex = 2;
            this.exit_button.Text = "Exit";
            this.exit_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.exit_button.UseVisualStyleBackColor = false;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // home_button
            // 
            this.home_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            this.home_button.FlatAppearance.BorderSize = 0;
            this.home_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.home_button.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home_button.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.home_button.Location = new System.Drawing.Point(3, 400);
            this.home_button.Name = "home_button";
            this.home_button.Size = new System.Drawing.Size(186, 60);
            this.home_button.TabIndex = 1;
            this.home_button.Text = "Home    ";
            this.home_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.home_button.UseVisualStyleBackColor = false;
            this.home_button.Click += new System.EventHandler(this.home_button_Click);
            // 
            // setting_button
            // 
            this.setting_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(43)))), ((int)(((byte)(58)))));
            this.setting_button.FlatAppearance.BorderSize = 0;
            this.setting_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setting_button.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setting_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.setting_button.Location = new System.Drawing.Point(0, 520);
            this.setting_button.Name = "setting_button";
            this.setting_button.Size = new System.Drawing.Size(186, 60);
            this.setting_button.TabIndex = 0;
            this.setting_button.Text = "Settings";
            this.setting_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.setting_button.UseVisualStyleBackColor = false;
            this.setting_button.Click += new System.EventHandler(this.setting_button_Click);
            // 
            // home_form_load_panel
            // 
            this.home_form_load_panel.Location = new System.Drawing.Point(186, 0);
            this.home_form_load_panel.Name = "home_form_load_panel";
            this.home_form_load_panel.Size = new System.Drawing.Size(1201, 640);
            this.home_form_load_panel.TabIndex = 2;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            // 
            // navigation_panel
            // 
            this.navigation_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(84)))), ((int)(((byte)(114)))));
            this.navigation_panel.Location = new System.Drawing.Point(0, 400);
            this.navigation_panel.Name = "navigation_panel";
            this.navigation_panel.Size = new System.Drawing.Size(5, 60);
            this.navigation_panel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1387, 640);
            this.Controls.Add(this.home_form_load_panel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.program_load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button setting_button;
        private System.Windows.Forms.Button home_button;
        private System.Windows.Forms.Panel home_form_load_panel;
        private System.Windows.Forms.Button exit_button;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.ComponentModel.BackgroundWorker backgroundWorker2;
        public System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button calib_button;
        private System.Windows.Forms.Panel navigation_panel;
    }
}

