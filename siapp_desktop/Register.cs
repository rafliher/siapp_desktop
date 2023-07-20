using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siapp_desktop
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string fullname = fullnameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            string confirmPassword = confirmTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter all data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!String.Equals(password, confirmPassword))
            {
                MessageBox.Show("Please enter your password twice exactly the same.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var httpClient = new HttpClient();
                var jsonContent = new StringContent(
                    $"{{ \"username\": \"{username}\", \"fullname\": \"{fullname}\", \"email\": \"{email}\", \"password\": \"{password}\" }}",
                    Encoding.UTF8,
                    "application/json");

                var response = await httpClient.PostAsync("http://localhost:8080/api/auth/signup", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Your account has been created. Please log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Login login = new Login();
                    login.ShowDialog();
                    this.Close();
                }
                else
                {
                    String errorJson = await response.Content.ReadAsStringAsync();
                    String errorMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(errorJson).message;
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("An error occurred while communicating with the server. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
    }
}
