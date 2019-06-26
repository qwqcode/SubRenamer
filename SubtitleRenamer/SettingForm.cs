using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SubtitleRenamer
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();

            MainSettings.Default.PropertyChanged += new PropertyChangedEventHandler(MainSettings_PropertyChanged);
        }

        private void MainSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainSettings.Default.Save();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            VersionText.Text = "v" + Application.ProductVersion.ToString();
        }

        private void UpdateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/qwqcode/SubtitleRenamer/releases");
        }

        private void GithubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/qwqcode/SubtitleRenamer");
        }

        private void AuthorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/qwqcode");
        }
    }
}
