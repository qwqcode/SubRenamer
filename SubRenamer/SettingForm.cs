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
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            VersionText.Text = "v" + Application.ProductVersion.ToString();

            RawSubtitleBuckup.Checked = AppSettings.RawSubtitleBuckup;
            OpenFolderFinished.Checked = AppSettings.OpenFolderFinished;
            ListItemRemovePrompt.Checked = AppSettings.ListItemRemovePrompt;
            ListShowFileFullName.Checked = AppSettings.ListShowFileFullName;

            foreach (Control c in Controls)//遍历groupBox1内的所有控件
            {
                if (c is CheckBox)//只遍历CheckBox控件
                {
                    var cb = (CheckBox)c;
                    // cb.Checked = AppSettings.IniFile.Read(cb.Name);
                    cb.CheckStateChanged += new EventHandler(Setting_CheckedChanged);
                }
            }
        }

        private void Setting_CheckedChanged(object sender, EventArgs e)
        {
            var cb = ((CheckBox)sender);
            AppSettings.IniFile.Write(cb.Name, cb.Checked ? "1" : "0");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ListItemRemovePrompt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RawSubtitleBuckup_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OpenFolderFinished_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
