using System;
using System.Windows.Forms;

namespace siapp_desktop
{
    public partial class PassphraseCreatePrompt : Form
    {
        // Public property to access the passphrase outside the class
        public string Passphrase { get; private set; }

        public PassphraseCreatePrompt()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            string passphrase = passphraseTextBox.Text;
            string confirmPassphrase = confirmTextBox.Text;

            // Validate that both passphrases are not empty and are the same
            if (string.IsNullOrEmpty(passphrase) || string.IsNullOrEmpty(confirmPassphrase))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (passphrase != confirmPassphrase)
            {
                MessageBox.Show("Passphrases do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set the passphrase property and close the form with DialogResult.OK
            Passphrase = passphrase;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Close the form with DialogResult.Cancel
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
