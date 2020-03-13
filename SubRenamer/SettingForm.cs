using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SubRenamer
{
    public partial class SettingForm : Form
    {
        private readonly MainForm _mainForm;

        public SettingForm(MainForm mainForm)
        {
            _mainForm = mainForm;
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
            Process.Start("https://github.com/qwqcode/SubRenamer/releases");
        }

        private void GithubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/qwqcode/SubRenamer");
        }

        private void AuthorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/qwqcode");
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/qwqcode/SubRenamer/issues/new");
        }

        private void ListShowFileFullNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _mainForm.RefreshFileListUi();
        }
    }
}
