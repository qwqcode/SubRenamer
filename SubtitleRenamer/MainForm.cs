using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SubtitleRenamer
{
    public partial class MainForm : Form
    {
        private static SettingForm settingForm;

        public MainForm()
        {
            InitializeComponent();
            settingForm = new SettingForm();
        }
        
        // 窗口加载完后
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFilesByPath(Application.StartupPath);
        }

        private void PathSelBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog PathSelDialog = new FolderBrowserDialog();
            if (OpenPath != null)
            {
                PathSelDialog.SelectedPath = OpenPath;
            }
            PathSelDialog.ShowDialog();
            LoadFilesByPath(PathSelDialog.SelectedPath);
        }

        private void LoadFilesByPath(string path)
        {
            OpenPath = path;
            PathTextBox.Text = OpenPath;
            // 刷新文件列表
            ReloadFiles();
        }

        /// <summary>
        /// 输入对话框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static string InputDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                TopMost = true,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 460 };
            TextBox textBox = new TextBox() { Left = 20, Top = 40, Width = 460 };
            Button confirmation = new Button() { Text = "完成", Left = 360, Width = 120, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void FileListBox_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            int index = listBox.IndexFromPoint(e.Location);
            // Check if the index is valid.
            if (index != -1 && index < listBox.Items.Count)
            {
                MainToolTip.Active = true;
                // Check if the ToolTip's text isn't already the one
                // we are now processing.
                if (MainToolTip.GetToolTip(listBox) != listBox.Items[index].ToString().Trim())
                {
                    // If it isn't, then a new item needs to be
                    // displayed on the toolTip. Update it.
                    MainToolTip.SetToolTip(listBox, listBox.Items[index].ToString().Trim());
                }
            } else
            {
                MainToolTip.Active = false;
            }
        }

        private void CopyrightText_Click(object sender, EventArgs e)
        {
            Process.Start("https://qwqaq.com/?from=SubtitleRenamer");
        }

        private void StartEasyBtn_Click(object sender, EventArgs e)
        {
            StartRename();
        }

        private void SettingBtn_Click(object sender, EventArgs e)
        {
            settingForm.ShowDialog();
        }
    }
}
