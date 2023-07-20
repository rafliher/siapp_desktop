using System.Windows.Forms;
using System;

namespace siapp_desktop
{
    partial class RevokeConfirmationDialog : Form
    {
        public string UserConfirmation { get; private set; }

        public RevokeConfirmationDialog()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            // Enable or disable the continue button based on user input
            btnContinue.Enabled = txtInput.Text.Trim().ToUpper() == "REVOKE";
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            UserConfirmation = txtInput.Text.Trim().ToUpper();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UserConfirmation = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
