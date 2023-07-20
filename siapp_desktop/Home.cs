﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siapp_desktop
{
    public partial class Home : Form
    {
        private String _accessToken;
        private const string ApiBaseUrl = "http://localhost:8080/api/";
        public Home(string accessToken)
        {
            InitializeComponent();
            this._accessToken = accessToken;
        }

        private async void Home_Load(object sender, EventArgs e)
        {
            string username = await GetUsernameFromApi(_accessToken);
            if (!string.IsNullOrEmpty(username))
            {
                helloLabel.Text = "Hello, " + username + "!";
            }
            else
            {
                helloLabel.Text = "Hello, User!";
                MessageBox.Show("Failed to fetch username from the API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string certificate = await GetCertificateFromApi(_accessToken);
            if (!string.IsNullOrEmpty(certificate))
            {
                // Certificate found, show the details in labels
                byte[] bytes = Encoding.ASCII.GetBytes(certificate);
                var x509Certificate = new X509Certificate2(bytes);

                string subject = x509Certificate.Subject;

                // Split the subject into its different components
                string[] subjectComponents = subject.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                string email = string.Empty;
                string commonName = string.Empty;
                string organization = string.Empty;
                string organizationalUnit = string.Empty;

                // Process each subject component
                foreach (string component in subjectComponents)
                {
                    // Split the component into its key-value pair
                    string[] keyValue = component.Trim().Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                    if (keyValue.Length == 2)
                    {
                        // Check the key and extract the value accordingly
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();

                        switch (key)
                        {
                            case "E":
                                email = value;
                                break;
                            case "CN":
                                commonName = value;
                                break;
                            case "O":
                                organization = value;
                                break;
                            case "OU":
                                organizationalUnit = value;
                                break;
                                // Add more cases for other subject components as needed
                        }
                    }
                }

                // Format the organization and organizational unit if they are not empty
                string organizationInfo = string.Empty;
                if (!string.IsNullOrEmpty(organization) && !string.IsNullOrEmpty(organizationalUnit))
                {
                    organizationInfo = organization + " - " + organizationalUnit;
                }
                else if (!string.IsNullOrEmpty(organization))
                {
                    organizationInfo = organization;
                }
                else if (!string.IsNullOrEmpty(organizationalUnit))
                {
                    organizationInfo = organizationalUnit;
                }

                certNameLabel.Text = commonName;
                certEmailLabel.Text = email;
                certOrgLabel.Text = organizationInfo;
                certExpLabel.Text = "Expired: " + x509Certificate.NotAfter.ToString();

                // Show the revoke button and hide the create button
                certCreateButton.Visible = false;
                certRevokeButton.Visible = true;
                certNameLabel.Visible = true;
                certEmailLabel.Visible = true;
                certOrgLabel.Visible = true;
                certExpLabel.Visible = true;
            }
            else
            {
                // Certificate not found, hide the details labels and show the create button
                certCreateButton.Visible = true;
                certRevokeButton.Visible = false;
                certNameLabel.Visible = false;
                certEmailLabel.Visible = false;
                certOrgLabel.Visible = false;
                certExpLabel.Visible = false;
            }
        }

        private async void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var passwordChangeForm = new PasswordChangeForm())
            {
                var result = passwordChangeForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string oldPassword = passwordChangeForm.OldPassword;
                    string newPassword = passwordChangeForm.NewPassword;

                    if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                    {
                        MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (newPassword != passwordChangeForm.ConfirmPassword)
                    {
                        MessageBox.Show("New password and confirmation password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var changePasswordResponse = await ChangePassword(oldPassword, newPassword);

                    if (changePasswordResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Decode the error message from the response
                        string errorJson = await changePasswordResponse.Content.ReadAsStringAsync();
                        string errorMessage = JsonConvert.DeserializeObject<dynamic>(errorJson).message;
                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void peekPassphraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var passwordForm = new PasswordForm())
                {
                    // Show the PasswordChangeForm as a dialog and get the result when the form is closed
                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        string password = passwordForm.Password;

                        if (!string.IsNullOrEmpty(password))
                        {
                            // Create the JSON payload
                            var payload = new
                            {
                                password = password
                            };

                            using (var httpClient = new HttpClient())
                            {
                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                                // Send the POST request to /api/user/passphrase
                                var response = await httpClient.PostAsync(ApiBaseUrl + "user/passphrase", new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

                                if (response.IsSuccessStatusCode)
                                {
                                    // Get the passphrase from the response JSON
                                    var jsonResponse = await response.Content.ReadAsStringAsync();
                                    JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                                    var passphrase = jsonObject["passphrase"]?.ToString();

                                    // Show a dialog with the passphrase information
                                    using (var passphraseDialog = new PassphraseDialog(passphrase))
                                    {
                                        passphraseDialog.ShowDialog();
                                    }
                                }
                                else
                                {
                                    // Handle other error cases here
                                    var errorJson = await response.Content.ReadAsStringAsync();
                                    JObject jsonObject = JsonConvert.DeserializeObject<JObject>(errorJson);
                                    var errorMessage = jsonObject["message"]?.ToString();
                                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter your current password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call
                MessageBox.Show("An error occurred while fetching the passphrase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void certCreateButton_Click(object sender, EventArgs e)
        {

        }

        private void certRevokeButton_Click(object sender, EventArgs e)
        {

        }

        private void fileSignButton_Click(object sender, EventArgs e)
        {

        }

        private void fileVerifyButton_Click(object sender, EventArgs e)
        {

        }

        private async Task<string> GetUsernameFromApi(string accessToken)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await httpClient.GetAsync(ApiBaseUrl + "user");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonContent = await response.Content.ReadAsStringAsync();
                        var username = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonContent).data.username;
                        return username;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call
                return null;
            }
        }

        private async Task<string> GetCertificateFromApi(string accessToken)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await httpClient.GetAsync(ApiBaseUrl + "certif/cert");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonContent = await response.Content.ReadAsStringAsync();
                        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonContent);
                        var certBase64 = jsonObject["cert"]?.ToString();

                        // Decode the Base64-encoded certificate
                        byte[] certBytes = Convert.FromBase64String(certBase64);
                        string certificate = Encoding.UTF8.GetString(certBytes);

                        fileSignButton.Enabled = true;
                        fileVerifyButton.Enabled = true;
                        return certificate;
                    }
                    else if (response.StatusCode == (System.Net.HttpStatusCode)428)
                    {
                        // No certificate found, return null
                        fileVerifyButton.Enabled = true;
                        return null;
                    }
                    else
                    {
                        // Handle other error cases here
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call
                return null;
            }
        }

        private async Task<HttpResponseMessage> ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Set the authorization header using the access token
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                    // Prepare the request payload
                    var payload = new
                    {
                        password = oldPassword,
                        newpassword = newPassword
                    };

                    // Serialize the payload to JSON
                    string jsonPayload = JsonConvert.SerializeObject(payload);

                    // Prepare the request content
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Send the POST request to change the password
                    var response = await httpClient.PutAsync(ApiBaseUrl + "user/password", content);
                    return response;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call
                return null;
            }
        }

    }
}