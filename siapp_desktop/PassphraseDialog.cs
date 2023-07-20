using System.Windows.Forms;
using System;

namespace siapp_desktop
{
    partial class PassphraseDialog : Form
    {
        private string passphrase;

        public PassphraseDialog(string passphrase)
        {
            InitializeComponent();
            this.passphrase = passphrase;
            txtPassphrase.Text = passphrase;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(passphrase);
            MessageBox.Show("Passphrase copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
