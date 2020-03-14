using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SubRenamer
{
    public partial class VsItemEditor : Form
    {
        private readonly MainForm mainForm;
        private readonly VsItem vsItem;

        public VsItemEditor(MainForm mainForm, VsItem vsItem)
        {
            this.mainForm = mainForm;
            this.vsItem = vsItem;
            InitializeComponent();
        }

        private void VsItemEditor_Load(object sender, EventArgs e)
        {
            if (vsItem == null) return;
            MatchKey_TextBox.Text = vsItem.MatchKey ?? "";

            Video_TextBox.Text = (vsItem.VideoFile != null) ? vsItem.VideoFile.FullName : "";
            Video_TextBox.SelectionStart = Video_TextBox.Text.Length;

            Sub_TextBox.Text = (vsItem.SubFile != null) ? vsItem.SubFile.FullName : "";
            Sub_TextBox.SelectionStart = Sub_TextBox.Text.Length;
        }

        private void Video_SelectFileBtn_Click(object sender, EventArgs e)
        {
            var filename = OpenFileSelectDialog(MainForm.VideoExts);
            if (filename != null)
                Video_TextBox.Text = filename;
            Video_TextBox.SelectionStart = Video_TextBox.Text.Length;
        }

        private void Sub_SelectFileBtn_Click(object sender, EventArgs e)
        {
            var filename = OpenFileSelectDialog(MainForm.SubExts);
            if (filename != null)
                Sub_TextBox.Text = filename;
            Sub_TextBox.SelectionStart = Sub_TextBox.Text.Length;
        }

        private string OpenFileSelectDialog(List<string> exts)
        {
            using (var fbd = new CommonOpenFileDialog()
            {
            })
            {
                fbd.Filters.Add(new CommonFileDialogFilter("媒体文件", string.Join(";", exts)));
                var result = fbd.ShowDialog();

                if (result == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    return fbd.FileName;
                }
            }
            return null;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                _Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _Save()
        {
            var match = MatchKey_TextBox.Text.Trim();
            var video = Video_TextBox.Text.Trim();
            var sub = Sub_TextBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(match) && vsItem.MatchKey != match)
                vsItem.MatchKey = match;

            if (!string.IsNullOrWhiteSpace(video) && (vsItem.VideoFile == null || video != vsItem.VideoFile.FullName))
            {
                vsItem.VideoFile = new FileInfo(video);
                vsItem.Status = VsStatus.SubLack;
            }

            if (!string.IsNullOrWhiteSpace(sub) && (vsItem.SubFile == null || sub != vsItem.SubFile.FullName))
            {
                vsItem.SubFile = new FileInfo(sub);
                vsItem.Status = VsStatus.VideoLack;
            }

            if (vsItem.SubFile != null && vsItem.VideoFile != null)
                vsItem.Status = VsStatus.Ready;

            if (string.IsNullOrWhiteSpace(vsItem.MatchKey))
                vsItem.Status = VsStatus.Unmatched;

            this.mainForm.RefreshFileListUi();

            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
