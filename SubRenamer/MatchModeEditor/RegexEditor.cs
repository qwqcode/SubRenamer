using SubRenamer.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SubRenamer.Global;

namespace SubRenamer.MatchModeEditor
{
    public partial class RegexEditor : Form
    {
        private MainForm mainForm;

        public RegexEditor(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void RegexEditor_Load(object sender, EventArgs e)
        {
            if (mainForm.M_Regx_V != null)
                VideoRegex.Text = mainForm.M_Regx_V.ToString();
            if (mainForm.M_Regx_S != null)
                SubRegex.Text = mainForm.M_Regx_S.ToString();

            V_Test_OpenBtn.Click += (s1, e1) => {
                Utils.OpenFile(AppFileType.Video, opened: (fileName, fileType) =>
                {
                    V_TestStr.Text = fileName;
                    TestStrRematch(fileType);
                });
            };
            S_Test_OpenBtn.Click += (s1, e1) => {
                Utils.OpenFile(AppFileType.Sub, opened: (fileName, fileType) =>
                {
                    S_TestStr.Text = fileName;
                    TestStrRematch(fileType);
                });
            };

            VideoRegex.TextChanged += (s1, e1) => TestStrRematch(AppFileType.Video, false);
            SubRegex.TextChanged += (s1, e1) => TestStrRematch(AppFileType.Sub, false);
            V_TestStr.TextChanged += (s1, e1) => TestStrRematch(AppFileType.Video, false);
            S_TestStr.TextChanged += (s1, e1) => TestStrRematch(AppFileType.Sub, false);
        }

        private Regex GetRegexInstance(AppFileType FileType, bool displayAlert = true)
        {
            string regxStr = "";
            if (FileType == AppFileType.Video) regxStr = VideoRegex.Text;
            else if (FileType == AppFileType.Sub) regxStr = SubRegex.Text;

            if (string.IsNullOrWhiteSpace(regxStr)) return null;

            Regex regex = null;
            try
            {
                regex = new Regex(regxStr.Trim());
            }
            catch (Exception ex)
            {
                if (displayAlert)
                {
                    var label = "";
                    if (FileType == AppFileType.Video) label = "视频";
                    else if (FileType == AppFileType.Sub) label = "字幕";
                    MessageBox.Show($"{label} 正则语法有误 {ex.Message}{Environment.NewLine}{ex.StackTrace}", "正则语法错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return regex;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            mainForm.M_Regx_V = GetRegexInstance(AppFileType.Video);
            mainForm.M_Regx_S = GetRegexInstance(AppFileType.Sub);

            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TestStrRematch(AppFileType FileType, bool displayAlert = true)
        {
            Regex regex = GetRegexInstance(FileType, displayAlert);
            if (FileType == AppFileType.Video)
            {
                V_TestResult.Text = MainForm.GetMatchKeyByRegex(V_TestStr.Text.Trim(), regex);
            }
            else if (FileType == AppFileType.Sub)
            {
                S_TestResult.Text = MainForm.GetMatchKeyByRegex(S_TestStr.Text.Trim(), regex);
            }
        }

        private void RegexTestLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://regexr.com/");
        }

        private void LearnRegexLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ziishaned/learn-regex/blob/master/translations/README-cn.md");
        }
    }
}
