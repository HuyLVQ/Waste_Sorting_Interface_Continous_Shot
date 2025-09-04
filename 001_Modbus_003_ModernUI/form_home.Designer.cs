namespace _001_Modbus_003_ModernUI
{
    partial class form_home
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
            this.data_groupBox = new System.Windows.Forms.GroupBox();
            this.stop_classifier_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.picture_groupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.data_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.picture_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // data_groupBox
            // 
            this.data_groupBox.Controls.Add(this.stop_classifier_button);
            this.data_groupBox.Controls.Add(this.stop_button);
            this.data_groupBox.Controls.Add(this.dataGridView1);
            this.data_groupBox.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data_groupBox.Location = new System.Drawing.Point(901, 60);
            this.data_groupBox.Name = "data_groupBox";
            this.data_groupBox.Size = new System.Drawing.Size(291, 562);
            this.data_groupBox.TabIndex = 9;
            this.data_groupBox.TabStop = false;
            this.data_groupBox.Text = "Predicted Data";
            // 
            // stop_classifier_button
            // 
            this.stop_classifier_button.Enabled = false;
            this.stop_classifier_button.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_classifier_button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.stop_classifier_button.Location = new System.Drawing.Point(7, 493);
            this.stop_classifier_button.Name = "stop_classifier_button";
            this.stop_classifier_button.Size = new System.Drawing.Size(172, 33);
            this.stop_classifier_button.TabIndex = 2;
            this.stop_classifier_button.Text = "Stop Classifier";
            this.stop_classifier_button.UseVisualStyleBackColor = true;
            this.stop_classifier_button.Click += new System.EventHandler(this.stop_classifier_button_click);
            // 
            // stop_button
            // 
            this.stop_button.Enabled = false;
            this.stop_button.Font = new System.Drawing.Font("Nirmala UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.stop_button.Location = new System.Drawing.Point(7, 436);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(172, 33);
            this.stop_button.TabIndex = 1;
            this.stop_button.Text = "Stop Communication";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(7, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(278, 361);
            this.dataGridView1.TabIndex = 0;
            // 
            // picture_groupBox
            // 
            this.picture_groupBox.Controls.Add(this.pictureBox);
            this.picture_groupBox.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picture_groupBox.Location = new System.Drawing.Point(16, 60);
            this.picture_groupBox.Name = "picture_groupBox";
            this.picture_groupBox.Size = new System.Drawing.Size(879, 562);
            this.picture_groupBox.TabIndex = 8;
            this.picture_groupBox.TabStop = false;
            this.picture_groupBox.Text = "Classified Image";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 39);
            this.label1.TabIndex = 7;
            this.label1.Text = "Home";
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
            // form_home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1201, 640);
            this.Controls.Add(this.data_groupBox);
            this.Controls.Add(this.picture_groupBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_home";
            this.Text = "form_home";
            this.data_groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.picture_groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox data_groupBox;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox picture_groupBox;
        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button stop_button;
        public System.Windows.Forms.Button stop_classifier_button;
    }
}