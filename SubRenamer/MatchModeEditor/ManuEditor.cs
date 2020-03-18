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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
