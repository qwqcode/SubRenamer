using MS.WindowsAPICodePack.Internal;
using SubRenamer.Lib;
using System;
using System.Diagnostics;
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
            ListItemRemovePrompt.Checked = AppSettings.ListItemRemovePrompt;
            ListShowFileFullName.Checked = AppSettings.ListShowFileFullName;
            RenameVideo.Checked = AppSettings.RenameVideo;

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

        private void UpdateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/qwqcode/SubRenamer/releases");

        private void GithubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/qwqcode/SubRenamer");

        private void AuthorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/qwqcode");

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/qwqcode/SubRenamer/issues/new");

        private void ListShowFileFullNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _mainForm.RefreshFileListUi();
        }

        private void BlogLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Program.OpenAuthorBlog();

        private void RenameVideo_CheckedChanged(object sender, EventArgs e)
        {
            _mainForm.RefreshFileListUi();
        }

        public void LoadDefaultExtensions()
        {
            foreach (var i in Global.VideoExts) listBoxVideoExtension.Items.Add(i);
            foreach (var i in Global.SubExts) listBoxSubExtension.Items.Add(i);

        }

        private void AddVideoExtension(object sender, EventArgs e)
        {
            string input = "";
            var result = InputBox.Input("输入视频扩展名（以.开头）", "", ref input);
            if (result.Equals(DialogResult.Cancel)) return;
            input = input.Trim();
            if (!input.StartsWith("."))
            {
                MessageBox.Show("请以.开头！");
                return;
            }
            Global.VideoExts.Add(input);
            listBoxVideoExtension.Items.Add(input);
        }

        private void DeleteVideoExtension(object sender, EventArgs e)
        {
            if (listBoxVideoExtension.Items.Count > 0)
            {
                Global.VideoExts.Remove(listBoxVideoExtension.SelectedItem.ToString());
                listBoxVideoExtension.Items.RemoveAt(listBoxVideoExtension.SelectedIndex);
                if (listBoxVideoExtension.Items.Count > 0)
                    listBoxVideoExtension.SelectedIndex = 0;
            }
        }

        private void AddSubExtension(object sender, EventArgs e)
        {
            string input = "";
            var result = InputBox.Input("输入字幕扩展名（以.开头）", "", ref input);
            if (result.Equals(DialogResult.Cancel)) return;
            input = input.Trim();
            if (!input.StartsWith("."))
            {
                MessageBox.Show("请以.开头！");
                return;
            }
            Global.SubExts.Add(input);
            listBoxSubExtension.Items.Add(input);
        }

        private void DeleteSubExtension(object sender, EventArgs e)
        {
            if (listBoxSubExtension.Items.Count > 0)
            {
                Global.SubExts.Remove(listBoxSubExtension.SelectedItem.ToString());
                listBoxSubExtension.Items.RemoveAt(listBoxSubExtension.SelectedIndex);
                if (listBoxSubExtension.Items.Count > 0)
                    listBoxSubExtension.SelectedIndex = 0;
            }
        }
    }
}
