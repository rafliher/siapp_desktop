namespace siapp_desktop
{
    partial class Home
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
            this.helloLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.certCreateButton = new System.Windows.Forms.Button();
            this.certRevokeButton = new System.Windows.Forms.Button();
            this.certExpLabel = new System.Windows.Forms.Label();
            this.certOrgLabel = new System.Windows.Forms.Label();
            this.certEmailLabel = new System.Windows.Forms.Label();
            this.certNameLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.manageAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peekPassphraseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSignButton = new System.Windows.Forms.Button();
            this.fileVerifyButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // helloLabel
            // 
            this.helloLabel.AutoSize = true;
            this.helloLabel.Location = new System.Drawing.Point(12, 35);
            this.helloLabel.Name = "helloLabel";
            this.helloLabel.Size = new System.Drawing.Size(74, 16);
            this.helloLabel.TabIndex = 0;
            this.helloLabel.Text = "Hello, user!";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.certCreateButton);
            this.groupBox1.Controls.Add(this.certRevokeButton);
            this.groupBox1.Controls.Add(this.certExpLabel);
            this.groupBox1.Controls.Add(this.certOrgLabel);
            this.groupBox1.Controls.Add(this.certEmailLabel);
            this.groupBox1.Controls.Add(this.certNameLabel);
            this.groupBox1.Location = new System.Drawing.Point(15, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 225);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Your Certificate";
            // 
            // certCreateButton
            // 
            this.certCreateButton.Location = new System.Drawing.Point(181, 90);
            this.certCreateButton.Name = "certCreateButton";
            this.certCreateButton.Size = new System.Drawing.Size(103, 34);
            this.certCreateButton.TabIndex = 5;
            this.certCreateButton.Text = "Create";
            this.certCreateButton.UseVisualStyleBackColor = true;
            this.certCreateButton.Visible = false;
            this.certCreateButton.Click += new System.EventHandler(this.certCreateButton_Click);
            // 
            // certRevokeButton
            // 
            this.certRevokeButton.Location = new System.Drawing.Point(333, 186);
            this.certRevokeButton.Name = "certRevokeButton";
            this.certRevokeButton.Size = new System.Drawing.Size(126, 33);
            this.certRevokeButton.TabIndex = 4;
            this.certRevokeButton.Text = "Revoke";
            this.certRevokeButton.UseVisualStyleBackColor = true;
            this.certRevokeButton.Click += new System.EventHandler(this.certRevokeButton_Click);
            // 
            // certExpLabel
            // 
            this.certExpLabel.AutoSize = true;
            this.certExpLabel.Location = new System.Drawing.Point(13, 137);
            this.certExpLabel.Name = "certExpLabel";
            this.certExpLabel.Size = new System.Drawing.Size(62, 16);
            this.certExpLabel.TabIndex = 3;
            this.certExpLabel.Text = "Exp Date";
            // 
            // certOrgLabel
            // 
            this.certOrgLabel.AutoSize = true;
            this.certOrgLabel.Location = new System.Drawing.Point(13, 99);
            this.certOrgLabel.Name = "certOrgLabel";
            this.certOrgLabel.Size = new System.Drawing.Size(106, 16);
            this.certOrgLabel.TabIndex = 2;
            this.certOrgLabel.Text = "Organization Info";
            // 
            // certEmailLabel
            // 
            this.certEmailLabel.AutoSize = true;
            this.certEmailLabel.Location = new System.Drawing.Point(13, 74);
            this.certEmailLabel.Name = "certEmailLabel";
            this.certEmailLabel.Size = new System.Drawing.Size(40, 16);
            this.certEmailLabel.TabIndex = 1;
            this.certEmailLabel.Text = "email";
            // 
            // certNameLabel
            // 
            this.certNameLabel.AutoSize = true;
            this.certNameLabel.Location = new System.Drawing.Point(13, 47);
            this.certNameLabel.Name = "certNameLabel";
            this.certNameLabel.Size = new System.Drawing.Size(62, 16);
            this.certNameLabel.TabIndex = 0;
            this.certNameLabel.Text = "Fullname";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageAccountToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(494, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // manageAccountToolStripMenuItem
            // 
            this.manageAccountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.peekPassphraseToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.manageAccountToolStripMenuItem.Name = "manageAccountToolStripMenuItem";
            this.manageAccountToolStripMenuItem.Size = new System.Drawing.Size(135, 24);
            this.manageAccountToolStripMenuItem.Text = "Manage Account";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // peekPassphraseToolStripMenuItem
            // 
            this.peekPassphraseToolStripMenuItem.Name = "peekPassphraseToolStripMenuItem";
            this.peekPassphraseToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.peekPassphraseToolStripMenuItem.Text = "Peek Passphrase";
            this.peekPassphraseToolStripMenuItem.Click += new System.EventHandler(this.peekPassphraseToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // fileSignButton
            // 
            this.fileSignButton.Enabled = false;
            this.fileSignButton.Location = new System.Drawing.Point(15, 305);
            this.fileSignButton.Name = "fileSignButton";
            this.fileSignButton.Size = new System.Drawing.Size(197, 96);
            this.fileSignButton.TabIndex = 3;
            this.fileSignButton.Text = "Sign a File";
            this.fileSignButton.UseVisualStyleBackColor = true;
            this.fileSignButton.Click += new System.EventHandler(this.fileSignButton_Click);
            // 
            // fileVerifyButton
            // 
            this.fileVerifyButton.Location = new System.Drawing.Point(283, 305);
            this.fileVerifyButton.Name = "fileVerifyButton";
            this.fileVerifyButton.Size = new System.Drawing.Size(197, 96);
            this.fileVerifyButton.TabIndex = 4;
            this.fileVerifyButton.Text = "Verify a File";
            this.fileVerifyButton.UseVisualStyleBackColor = true;
            this.fileVerifyButton.Click += new System.EventHandler(this.fileVerifyButton_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 413);
            this.Controls.Add(this.fileVerifyButton);
            this.Controls.Add(this.fileSignButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.helloLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label helloLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button certCreateButton;
        private System.Windows.Forms.Button certRevokeButton;
        private System.Windows.Forms.Label certExpLabel;
        private System.Windows.Forms.Label certOrgLabel;
        private System.Windows.Forms.Label certEmailLabel;
        private System.Windows.Forms.Label certNameLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem manageAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peekPassphraseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.Button fileSignButton;
        private System.Windows.Forms.Button fileVerifyButton;
    }
}