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
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private async void SigninButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var httpClient = new HttpClient();
                var jsonContent = new StringContent(
                    $"{{ \"username\": \"{username}\", \"password\": \"{password}\" }}",
                    Encoding.UTF8,
                    "application/json");

                var response = await httpClient.PostAsync("http://localhost:8080/api/auth/signin", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    String responseJson = await response.Content.ReadAsStringAsync();
                    String accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseJson).accessToken;
                    this.Hide();
                    Home home = new Home(accessToken);
                    home.ShowDialog();
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
            Register register = new Register();
            register.ShowDialog();
            this.Close();
        }
    }
}
