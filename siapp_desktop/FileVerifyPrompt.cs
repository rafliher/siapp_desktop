﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace siapp_desktop
{
    public partial class FileVerifyPrompt : Form
    {
        public string FileName { get; private set; }
        public string FilePath { get; private set; }

        public FileVerifyPrompt()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += PassphrasePrompt_DragEnter;
            DragDrop += PassphrasePrompt_DragDrop;
        }

        private void PassphrasePrompt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void PassphrasePrompt_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1 && Path.GetExtension(files[0]).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                FileName = Path.GetFileName(files[0]);
                FileNameLabel.Text = FileName;
                FilePath = files[0];
            }
            else
            {
                MessageBox.Show("Please drag only one PDF file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("Please select a PDF file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
