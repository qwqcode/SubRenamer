using SubRenamer.MatchModeEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubRenamer
{
    public partial class RuleEditor : Form
    {
        private MainForm mainForm;

        public RuleEditor(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void RuleEditor_Load(object sender, EventArgs e)
        {
            var curtMode = mainForm.CurtMatchMode;
            if (curtMode == MainForm.MatchMode.Auto)
                ModeBtn_Auto.Checked = true;
            else if (curtMode == MainForm.MatchMode.Manu)
                ModeBtn_Manu.Checked = true;
            else if (curtMode == MainForm.MatchMode.Regex)
                ModeBtn_Regex.Checked = true;
        }

        private void ModeBtn_Auto_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.CurtMatchMode = MainForm.MatchMode.Auto;
        }

        private void ModeBtn_Manu_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.CurtMatchMode = MainForm.MatchMode.Manu;
        }

        private void ModeBtn_Regex_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.CurtMatchMode = MainForm.MatchMode.Regex;
        }

        private void EditBtn_Manu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModeBtn_Manu.PerformClick();
            var form = new ManuEditor(mainForm);
            form.ShowDialog();
        }

        private void EditBtn_Regex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModeBtn_Regex.PerformClick();
            var form = new RegexEditor(mainForm);
            form.ShowDialog();
        }

        private void RuleEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.MatchVideoSub();
        }

        private void Copyright_Click(object sender, EventArgs e) => Program.OpenAuthorBlog();
    }
}
