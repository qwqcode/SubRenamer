using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.M_Regx_V = new Regex(VideoRegex.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"视频 正则语法有误 {ex.Message}{Environment.NewLine}{ex.StackTrace}", "正则语法错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                mainForm.M_Regx_S = new Regex(SubRegex.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"字幕 正则语法有误 {ex.Message}{Environment.NewLine}{ex.StackTrace}", "正则语法错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
