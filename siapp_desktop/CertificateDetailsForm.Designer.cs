namespace siapp_desktop
{
    partial class CertificateDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fullNameLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.organizationLabel = new System.Windows.Forms.Label();
            this.organizationalUnitLabel = new System.Windows.Forms.Label();
            this.countryLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.localityLabel = new System.Windows.Forms.Label();
            this.fullNameTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.organizationTextBox = new System.Windows.Forms.TextBox();
            this.organizationalUnitTextBox = new System.Windows.Forms.TextBox();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.stateTextBox = new System.Windows.Forms.TextBox();
            this.localityTextBox = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fullNameLabel
            // 
            this.fullNameLabel.AutoSize = true;
            this.fullNameLabel.Location = new System.Drawing.Point(30, 25);
            this.fullNameLabel.Name = "fullNameLabel";
            this.fullNameLabel.Size = new System.Drawing.Size(68, 16);
            this.fullNameLabel.TabIndex = 0;
            this.fullNameLabel.Text = "Full Name";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(30, 60);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(41, 16);
            this.emailLabel.TabIndex = 1;
            this.emailLabel.Text = "Email";
            // 
            // organizationLabel
            // 
            this.organizationLabel.AutoSize = true;
            this.organizationLabel.Location = new System.Drawing.Point(30, 95);
            this.organizationLabel.Name = "organizationLabel";
            this.organizationLabel.Size = new System.Drawing.Size(82, 16);
            this.organizationLabel.TabIndex = 2;
            this.organizationLabel.Text = "Organization";
            // 
            // organizationalUnitLabel
            // 
            this.organizationalUnitLabel.AutoSize = true;
            this.organizationalUnitLabel.Location = new System.Drawing.Point(30, 130);
            this.organizationalUnitLabel.Name = "organizationalUnitLabel";
            this.organizationalUnitLabel.Size = new System.Drawing.Size(119, 16);
            this.organizationalUnitLabel.TabIndex = 3;
            this.organizationalUnitLabel.Text = "Organizational Unit";
            // 
            // countryLabel
            // 
            this.countryLabel.AutoSize = true;
            this.countryLabel.Location = new System.Drawing.Point(30, 165);
            this.countryLabel.Name = "countryLabel";
            this.countryLabel.Size = new System.Drawing.Size(52, 16);
            this.countryLabel.TabIndex = 4;
            this.countryLabel.Text = "Country";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(30, 200);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(38, 16);
            this.stateLabel.TabIndex = 5;
            this.stateLabel.Text = "State";
            // 
            // localityLabel
            // 
            this.localityLabel.AutoSize = true;
            this.localityLabel.Location = new System.Drawing.Point(30, 235);
            this.localityLabel.Name = "localityLabel";
            this.localityLabel.Size = new System.Drawing.Size(53, 16);
            this.localityLabel.TabIndex = 6;
            this.localityLabel.Text = "Locality";
            // 
            // fullNameTextBox
            // 
            this.fullNameTextBox.Enabled = false;
            this.fullNameTextBox.Location = new System.Drawing.Point(180, 25);
            this.fullNameTextBox.Name = "fullNameTextBox";
            this.fullNameTextBox.Size = new System.Drawing.Size(250, 22);
            this.fullNameTextBox.TabIndex = 7;
            // 
            // emailTextBox
            // 
            this.emailTextBox.Enabled = false;
            this.emailTextBox.Location = new System.Drawing.Point(180, 60);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(250, 22);
            this.emailTextBox.TabIndex = 8;
            // 
            // organizationTextBox
            // 
            this.organizationTextBox.Location = new System.Drawing.Point(180, 95);
            this.organizationTextBox.Name = "organizationTextBox";
            this.organizationTextBox.Size = new System.Drawing.Size(250, 22);
            this.organizationTextBox.TabIndex = 9;
            // 
            // organizationalUnitTextBox
            // 
            this.organizationalUnitTextBox.Location = new System.Drawing.Point(180, 130);
            this.organizationalUnitTextBox.Name = "organizationalUnitTextBox";
            this.organizationalUnitTextBox.Size = new System.Drawing.Size(250, 22);
            this.organizationalUnitTextBox.TabIndex = 10;
            // 
            // countryComboBox
            // 
            this.countryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.countryComboBox.Enabled = false;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Items.AddRange(new object[] {
            "Indonesia",
            "Other"});
            this.countryComboBox.Location = new System.Drawing.Point(180, 165);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(250, 24);
            this.countryComboBox.TabIndex = 11;
            // 
            // stateTextBox
            // 
            this.stateTextBox.Enabled = false;
            this.stateTextBox.Location = new System.Drawing.Point(180, 200);
            this.stateTextBox.Name = "stateTextBox";
            this.stateTextBox.Size = new System.Drawing.Size(250, 22);
            this.stateTextBox.TabIndex = 12;
            // 
            // localityTextBox
            // 
            this.localityTextBox.Enabled = false;
            this.localityTextBox.Location = new System.Drawing.Point(180, 235);
            this.localityTextBox.Name = "localityTextBox";
            this.localityTextBox.Size = new System.Drawing.Size(250, 22);
            this.localityTextBox.TabIndex = 13;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(180, 275);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(100, 30);
            this.createButton.TabIndex = 14;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(330, 275);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // CertificateDetailsForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 318);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.localityTextBox);
            this.Controls.Add(this.stateTextBox);
            this.Controls.Add(this.countryComboBox);
            this.Controls.Add(this.organizationalUnitTextBox);
            this.Controls.Add(this.organizationTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.fullNameTextBox);
            this.Controls.Add(this.localityLabel);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.countryLabel);
            this.Controls.Add(this.organizationalUnitLabel);
            this.Controls.Add(this.organizationLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.fullNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CertificateDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Certificate Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fullNameLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label organizationLabel;
        private System.Windows.Forms.Label organizationalUnitLabel;
        private System.Windows.Forms.Label countryLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label localityLabel;
        private System.Windows.Forms.TextBox fullNameTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox organizationTextBox;
        private System.Windows.Forms.TextBox organizationalUnitTextBox;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.TextBox stateTextBox;
        private System.Windows.Forms.TextBox localityTextBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
