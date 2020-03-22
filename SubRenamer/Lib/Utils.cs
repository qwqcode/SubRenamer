using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SubRenamer.Global;

namespace SubRenamer.Lib
{
    class Utils
    {
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

        public static void OpenFile(AppFileType FileType, Action<string, AppFileType> opened)
        {
            using (var fbd = new CommonOpenFileDialog())
            {
                if (FileType == AppFileType.Video)
                    fbd.Filters.Add(new CommonFileDialogFilter("视频文件", string.Join(";", VideoExts.ToList())));
                else if (FileType == AppFileType.Sub)
                    fbd.Filters.Add(new CommonFileDialogFilter("字幕文件", string.Join(";", SubExts.ToList())));

                fbd.Filters.Add(new CommonFileDialogFilter("视频或字幕文件", string.Join(";", VideoExts.Concat(SubExts).ToList())));
                fbd.Filters.Add(new CommonFileDialogFilter("任何类型", "*.*"));
                var result = fbd.ShowDialog();

                if (result == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    var fileName = Path.GetFileName(fbd.FileName.Trim());
                    opened(fileName, FileType);
                }
            }
        }
    }
}
