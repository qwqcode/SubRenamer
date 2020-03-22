using Microsoft.WindowsAPICodePack.Dialogs;
using SubRenamer.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SubRenamer.Global;

namespace SubRenamer.MatchModeEditor
{
    public partial class ManuEditor : Form
    {
        private static readonly string MatchSign = "<X>".ToUpper();

        private static string V_Raw = null;
        private static string S_Raw = null;

        private string V_Begin = null;
        private string V_End = null;

        private string S_Begin = null;
        private string S_End = null;

        private MainForm mainForm;

        public ManuEditor(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void ManuEditor_Load(object sender, EventArgs e)
        {
            V_Tpl.TextChanged += (s1, e1) => { MatchRuleUpdated(AppFileType.Video); };
            S_Tpl.TextChanged += (s1, e1) => { MatchRuleUpdated(AppFileType.Sub); };

            V_OpenBtn.Click += (s1, e1) => {
                Utils.OpenFile(AppFileType.Video, opened: (fileName, fileType) =>
                {
                    V_Raw = fileName;
                    V_Tpl.Text = fileName;
                    MatchRuleUpdated(AppFileType.Video);
                });
            };
            S_OpenBtn.Click += (s1, e1) => {
                Utils.OpenFile(AppFileType.Sub, opened: (fileName, fileType) =>
                {
                    S_Raw = fileName;
                    S_Tpl.Text = fileName;
                    MatchRuleUpdated(AppFileType.Sub);
                });
            };

            var M_V_Begin = mainForm.M_Manu_V_Begin;
            var M_V_End = mainForm.M_Manu_V_End;
            if (!string.IsNullOrWhiteSpace(M_V_Begin) && !string.IsNullOrWhiteSpace(M_V_End))
            {
                V_Tpl.Text = $"{M_V_Begin}{MatchSign}{M_V_End}";
                MatchRuleUpdated(AppFileType.Video);
            }

            var M_S_Begin = mainForm.M_Manu_S_Begin;
            var M_S_End = mainForm.M_Manu_S_End;
            if (!string.IsNullOrWhiteSpace(M_S_Begin) && !string.IsNullOrWhiteSpace(M_S_End))
            {
                S_Tpl.Text = $"{M_S_Begin}{MatchSign}{M_S_End}";
                MatchRuleUpdated(AppFileType.Sub);
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            mainForm.M_Manu_V_Begin = V_Begin;
            mainForm.M_Manu_V_End = V_End;
            mainForm.M_Manu_S_Begin = S_Begin;
            mainForm.M_Manu_S_End = S_End;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MatchRuleUpdated(AppFileType FileType)
        {
            if (FileType == AppFileType.Video)
            {
                V_Begin = null;
                V_End = null;
                V_Matched.Text = "未匹配";
                var tpl = V_Tpl.Text.Trim();

                if (string.IsNullOrWhiteSpace(tpl)) return;
                var pos = tpl.ToUpper().IndexOf(MatchSign);
                if (pos <= -1) return;
                var afterPos = pos + MatchSign.Length;

                V_Begin = tpl.Substring(0, pos);
                V_End = tpl.Substring(afterPos, tpl.Length - afterPos);
                V_Matched.Text = "匹配结果: " + MainForm.GetMatchKeyByBeginEndStr(V_Raw, V_Begin, V_End);
            }
            else if (FileType == AppFileType.Sub)
            {
                S_Begin = null;
                S_End = null;
                S_Matched.Text = "未匹配";
                var tpl = S_Tpl.Text.Trim();

                if (string.IsNullOrWhiteSpace(tpl)) return;
                var pos = tpl.ToUpper().IndexOf(MatchSign);
                if (pos <= -1) return;
                var afterPos = pos + MatchSign.Length;

                S_Begin = tpl.Substring(0, pos);
                S_End = tpl.Substring(afterPos, tpl.Length - afterPos);
                S_Matched.Text = "匹配结果: " + MainForm.GetMatchKeyByBeginEndStr(S_Raw, S_Begin, S_End);
            }
        }
    }
}
