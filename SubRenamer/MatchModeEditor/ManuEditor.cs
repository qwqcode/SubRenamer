using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubRenamer.MatchModeEditor
{
    public partial class ManuEditor : Form
    {
        private MainForm mainForm;

        public ManuEditor(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void ManuEditor_Load(object sender, EventArgs e)
        {
            V_Begin.TextChanged += ResetTestTextBox;
            V_End.TextChanged += ResetTestTextBox;
            S_Begin.TextChanged += ResetTestTextBox;
            S_End.TextChanged += ResetTestTextBox;
            Test_V.TextChanged += (s1, e1) => { RefreshTestResult(); };
            Test_S.TextChanged += (s1, e1) => { RefreshTestResult(); };
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            mainForm.M_Manu_V_Begin = V_Begin.Text.Trim();
            mainForm.M_Manu_V_End = V_End.Text.Trim();
            mainForm.M_Manu_S_Begin = S_Begin.Text.Trim();
            mainForm.M_Manu_S_End = S_End.Text.Trim();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetTestTextBox(object sender, EventArgs e)
        {
            Test_V.Text = $"{V_Begin.Text.Trim()}集数{V_End.Text.Trim()}";
            Test_S.Text = $"{S_Begin.Text.Trim()}集数{S_End.Text.Trim()}";
            RefreshTestResult();
        }

        private void RefreshTestResult()
        {
            TestResult_V.Text = MainForm.GetMatchKeyByBeginEndStr(Test_V.Text.Trim(), V_Begin.Text.Trim(), V_End.Text.Trim());
            TestResult_S.Text = MainForm.GetMatchKeyByBeginEndStr(Test_S.Text.Trim(), S_Begin.Text.Trim(), S_End.Text.Trim());
        }
    }
}
