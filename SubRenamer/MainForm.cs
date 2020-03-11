using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SubRenamer
{
    public partial class MainForm : Form
    {
        private static SettingForm SettingForm;

        public MainForm()
        {
            InitializeComponent();
            Text += $" {Program.GetVersionStr()}"; // 追加版本号
            SettingForm = new SettingForm(); // 设置窗体
        }
        
        // 窗口加载完后
        private void MainForm_Load(object sender, EventArgs e)
        {
            /*LoadFilesByPath(Application.StartupPath);*/
            SetWindowTheme(this.FileListUi.Handle, "Explorer", null);
        }

        // 按钮 打开文件夹
        private void OpenFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Multiselect = true
            })
            {
                var result = fbd.ShowDialog();

                if (result == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    foreach (var folderPath in fbd.FileNames)
                    {
                        var folder = new DirectoryInfo(folderPath);
                        var files = folder.GetFiles("*");

                        // 视频文件
                        foreach (var file in files.Where(s => VideoExts.Contains(s.Extension.ToString().ToLower())))
                            VideoFileList.Add(file);
                        
                        // 字幕文件
                        foreach (var file in files.Where(s => SubExts.Contains(s.Extension.ToString().ToLower())))
                            SubFileList.Add(file);
                    }

                    MatchVideoSub();
                }
            }
            
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
            /*ListBox listBox = (ListBox)sender;
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
            }*/
        }

        // 点击 一键更改按钮
        private void StartBtn_Click(object sender, EventArgs e)
        {
            TaskDialog td = new TaskDialog();
            td.Caption = "一键改名操作";
            td.Text = "执行一键改名操作中，请稍后...";

            TaskDialogProgressBar tdp = new TaskDialogProgressBar(0, 100, 34);
            td.ProgressBar = tdp;

            td.StandardButtons = TaskDialogStandardButtons.Cancel;
            td.DetailsExpandedLabel = "查看详情";
            td.DetailsExpandedText = "23333333 - View informatio";

            td.Show();
            /*StartRename();*/
        }

        // 点击 顶部菜单 设置按钮
        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm.ShowDialog();
        }

        // 主窗体尺寸发生变化
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // 自适应文件列表字段宽度
            int calcPathInfoWidth = (FileListUi.Width - (FileListUi.Columns[0].Width + FileListUi.Columns[3].Width + 8)) / 2;
            FileListUi.Columns[1].Width = calcPathInfoWidth;
            FileListUi.Columns[2].Width = calcPathInfoWidth;
        }

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        private void CopyrightText_Click(object sender, EventArgs e)
        {
            Process.Start("https://qwqaq.com/?from=SubRenamer");
        }
    }
}
