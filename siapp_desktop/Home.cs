using Newtonsoft.Json.Linq;
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
using System.Runtime.ConstrainedExecution;
using System.IO;
using System.Diagnostics;

namespace siapp_desktop
{
    public partial class Home : Form
    {
        private String _accessToken;
        private const string ApiBaseUrl = "http://localhost:8080/api/";

        private string username, fullname, email;
        byte[] p12CertificateBytes;

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
            string certificate = await GetCertificateFromApi();
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

                // Fetch the p12 for signing
                p12CertificateBytes = await GetP12FromApi();
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

        private async void certCreateButton_Click(object sender, EventArgs e)
        {
            using (var certDetailsForm = new CertificateDetailsForm(fullname, email))
            {
                var result = certDetailsForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Prepare the JSON payload for certificate creation
                    var payload = new
                    {
                        organization = certDetailsForm.Organization,
                        organizationUnit = certDetailsForm.OrganizationalUnit,
                    };

                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                        // Send the POST request to /api/certif
                        var response = await httpClient.PostAsync(ApiBaseUrl + "certif", new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

                        if (response.IsSuccessStatusCode)
                        {
                            // Certificate creation successful
                            MessageBox.Show("Certificate created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Update the UI to show the certificate details
                            Home_Load(sender, e);

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
                            // Decode the error message from the response
                            var errorJson = await response.Content.ReadAsStringAsync();
                            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(errorJson);
                            var errorMessage = jsonObject["message"]?.ToString();
                            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void certRevokeButton_Click(object sender, EventArgs e)
        {
            using (var confirmDialog = new RevokeConfirmationDialog())
            {
                var result = confirmDialog.ShowDialog();

                if (result == DialogResult.OK && confirmDialog.UserConfirmation == "REVOKE")
                {
                    // User confirmed the revoke action, proceed with certificate revocation
                    var revokeResponse = await RevokeCertificate();

                    if (revokeResponse.IsSuccessStatusCode)
                    {
                        // Certificate revocation successful
                        MessageBox.Show("Certificate revoked successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Hide the certificate details and show the create button
                        certCreateButton.Visible = true;
                        certRevokeButton.Visible = false;
                        certNameLabel.Visible = false;
                        certEmailLabel.Visible = false;
                        certOrgLabel.Visible = false;
                        certExpLabel.Visible = false;
                    }
                    else
                    {
                        // Decode the error message from the response
                        var errorJson = await revokeResponse.Content.ReadAsStringAsync();
                        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(errorJson);
                        var errorMessage = jsonObject["message"]?.ToString();
                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void fileSignButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var passphrasePrompt = new PassphrasePrompt())
                {
                    // Show the PasswordChangeForm as a dialog and get the result when the form is closed
                    if (passphrasePrompt.ShowDialog() == DialogResult.OK)
                    {
                        string passphrase = passphrasePrompt.Passphrase;
                        string pdfPath = passphrasePrompt.FilePath;
                        string pdfName= passphrasePrompt.FileName;

                        string targetPath = CopyFileToBinaryDirectory(pdfPath);

                        if (!string.IsNullOrEmpty(passphrase))
                        {
                            X509Certificate2 p12Certificate = new X509Certificate2(p12CertificateBytes, passphrase);
                            new IronPdf.Signing.PdfSignature(p12CertificateBytes, passphrase).SignPdfFile(targetPath);
                            MessageBox.Show("File signed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(targetPath);
                        }
                        else
                        {
                            MessageBox.Show("Please enter your passphrase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while signing the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        var user = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonContent).data;
                        this.username = username;
                        this.fullname = user.fullname;
                        this.email = user.email;
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

        private async Task<string> GetCertificateFromApi()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

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
                        return certificate;
                    }
                    else if (response.StatusCode == (System.Net.HttpStatusCode)428)
                    {
                        // No certificate found, return null
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

        private async Task<byte[]> GetP12FromApi()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                    var response = await httpClient.GetAsync(ApiBaseUrl + "certif/p12");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonContent = await response.Content.ReadAsStringAsync();
                        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonContent);
                        var p12Base64 = jsonObject["p12"]?.ToString();

                        // Decode the Base64-encoded certificate
                        byte[] p12Bytes = Convert.FromBase64String(p12Base64);

                        return p12Bytes;
                    }
                    else if (response.StatusCode == (System.Net.HttpStatusCode)428)
                    {
                        // No certificate found, return null
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

        private async Task<HttpResponseMessage> RevokeCertificate()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Set the authorization header using the access token
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                    // Send the DELETE request to revoke the certificate
                    var response = await httpClient.DeleteAsync(ApiBaseUrl + "certif");

                    return response;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call
                return null;
            }
        }


        private String CopyFileToBinaryDirectory(string sourceFilePath)
        {
            try
            {
                if (File.Exists(sourceFilePath))
                {
                    // Get the filename without the extension
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFilePath);

                    // Get the file extension
                    string fileExtension = Path.GetExtension(sourceFilePath);

                    // Create the new filename with the "_signed" suffix
                    string newFileName = $"{fileNameWithoutExtension}_signed{fileExtension}";

                    // Get the binary directory path (the directory where the application is running)
                    string binaryDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    // Combine the binary directory with the new filename to get the destination file path
                    string destinationFilePath = Path.Combine(binaryDirectory, newFileName);

                    // Copy the file to the destination path
                    File.Copy(sourceFilePath, destinationFilePath, true);

                    return destinationFilePath;
                }
                else
                {
                    MessageBox.Show("Source file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while copying the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
