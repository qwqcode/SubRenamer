using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SubtitleRenamer
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();

            MainSettings.Default.PropertyChanged += new PropertyChangedEventHandler(MainSettings_PropertyChanged);
        }

        private void MainSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainSettings.Default.Save();
        }
    }
}
