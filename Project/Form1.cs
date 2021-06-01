using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Project
{
    public partial class ScanningMalware : Form
    {
        public ScanningMalware()
        {
            InitializeComponent();
        }

        public string GetMD5FromFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty).ToLower();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var md5signatures = File.ReadAllLines("MD5base.txt");
            if (md5signatures.Contains(tbMD5.Text))
            {
                lbStatus.Text = "Infected!";
                lbStatus.ForeColor = Color.Red;
            }

            else
            {
                lbStatus.Text = "Clean!";
                lbStatus.ForeColor = Color.Green;
            }
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All | *.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbMD5.Text = GetMD5FromFile(ofd.FileName);
                tbFile.Text = ofd.SafeFileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void еncryptedPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Encrypt encryptPass = new Encrypt();
            encryptPass.ShowDialog();
            this.Close();
        }
    }
}
