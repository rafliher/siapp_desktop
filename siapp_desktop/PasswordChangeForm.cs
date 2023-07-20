using System;
using System.Windows.Forms;

namespace siapp_desktop
{
    public partial class PasswordChangeForm : Form
    {
        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }
        public string ConfirmPassword { get; private set; }

        public PasswordChangeForm()
        {
            InitializeComponent();
        }

        private void PasswordChangeForm_Load(object sender, EventArgs e)
        {
            // Initialize form fields
            OldPasswordTextBox.Clear();
            NewPasswordTextBox.Clear();
            ConfirmPasswordTextBox.Clear();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // Get the values from the form fields
            OldPassword = OldPasswordTextBox.Text;
            NewPassword = NewPasswordTextBox.Text;
            ConfirmPassword = ConfirmPasswordTextBox.Text;

            // Perform additional validation as needed
            // For example, check for password complexity requirements, length, etc.

            // Validate that all fields are filled
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Close the form and return DialogResult.OK
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Close the form and return DialogResult.Cancel
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
