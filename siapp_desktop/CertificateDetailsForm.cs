using System.Windows.Forms;
using System;

namespace siapp_desktop
{
    public partial class CertificateDetailsForm : Form
    {
        private readonly string _fullName;
        private readonly string _email;

        public string FullName => _fullName;
        public string Email => _email;
        public string Organization => organizationTextBox.Text.Trim();
        public string OrganizationalUnit => organizationalUnitTextBox.Text.Trim();

        public string Country => "ID";
        public string State => "West Java";
        public string Locality => "Ciseeng";

        public CertificateDetailsForm(string fullName, string email)
        {
            InitializeComponent();

            _fullName = fullName;
            _email = email;

            // Fill the full name and email textboxes with the data obtained from /api/user
            fullNameTextBox.Text = _fullName;
            emailTextBox.Text = _email;

            // Set default values for country, state, and locality
            countryComboBox.SelectedIndex = 0; // Assuming "Indonesia" is the first item in the combobox
            stateTextBox.Text = "West Java";
            localityTextBox.Text = "Ciseeng";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
