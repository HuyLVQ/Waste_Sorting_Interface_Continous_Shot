using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;

namespace _001_Modbus_003_ModernUI
{
    public partial class form_home : Form
    {
        private Form1 mainForm;
        public form_home(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void stop_classifier_button_click(object sender, EventArgs e)
        {
            stop_classifier_button.Enabled = false;
            stop_classifier_button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
        }

        private void stop_button_click(object sender, EventArgs e)
        {
            try
            {
                this.mainForm.backgroundWorker1.CancelAsync();
                this.mainForm.backgroundWorker2.CancelAsync();
                this.mainForm.modbus_client.Disconnect();
                MessageBox.Show(this, "Modbus Client successfully disconnected", "User Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Enabled = true;
                this.stop_button.Enabled = false;
                this.stop_button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to disconnect Modbus Client\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
