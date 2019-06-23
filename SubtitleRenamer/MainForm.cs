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
        public MainForm()
        {
            InitializeComponent();
        }

        private void PathSelBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog PathSelDialog = new FolderBrowserDialog();
            if (OpenPath != null)
            {
                PathSelDialog.SelectedPath = OpenPath;
            }
            PathSelDialog.ShowDialog();
            PathTextBox.Text = OpenPath = PathSelDialog.SelectedPath;

            // 刷新文件列表
            ReloadFiles();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartRename();
        }

        private void MatchEpisodeBtn_Click(object sender, EventArgs e)
        {
            MatchEpisode();
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
    }
}
