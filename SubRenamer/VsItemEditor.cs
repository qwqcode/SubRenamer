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
using static SubRenamer.Global;

namespace SubRenamer
{
    public partial class VsItemEditor : Form
    {
        private readonly MainForm mainForm;
        private readonly List<VsItem> vsList;
        private VsItem vsItem;

        public VsItemEditor(MainForm mainForm, List<VsItem> vsList, VsItem vsItem)
        {
            this.mainForm = mainForm;
            this.vsList = vsList;
            this.vsItem = vsItem;
            InitializeComponent();

            MainToolTip.SetToolTip(PrevItemBtn, "上一个项目");
            MainToolTip.SetToolTip(NextItemBtn, "下一个项目");
            MainToolTip.SetToolTip(AddItemBtn, "新增一个项目");
            MainToolTip.SetToolTip(RemoveItemBtn, "删除此项目");
            MainToolTip.SetToolTip(Video_ClearBtn, "删除视频");
            MainToolTip.SetToolTip(Video_SelectFileBtn, "选择新视频");
            MainToolTip.SetToolTip(Sub_ClearBtn, "删除字幕");
            MainToolTip.SetToolTip(Sub_SelectFileBtn, "选择新字幕");
        }

        private void VsItemEditor_Load(object sender, EventArgs e)
        {
            RefreshByVsItem();
        }

        private void RefreshByVsItem()
        {
            if (vsItem == null) return;
            MatchKey_TextBox.Text = vsItem.MatchKey ?? "";

            Video_TextBox.Text = (vsItem.Video != null) ? vsItem.Video : "";
            Video_TextBox.SelectionStart = Video_TextBox.Text.Length;

            Sub_TextBox.Text = (vsItem.Sub != null) ? vsItem.Sub : "";
            Sub_TextBox.SelectionStart = Sub_TextBox.Text.Length;

            PageNum.Text = $"{vsList.IndexOf(vsItem) + 1}/{vsList.Count}";
        }

        private void MatchKey_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MatchKey_TextBox.Text)) return;
            vsItem.MatchKey = MatchKey_TextBox.Text.Trim();
            UpdateStatus();
        }

        private void Video_SelectFileBtn_Click(object sender, EventArgs e)
        {
            var filename = OpenFileSelectDialog(VideoExts);
            if (filename != null)
            {
                Video_TextBox.Text = filename;
                vsItem.Video = !string.IsNullOrWhiteSpace(filename) ? filename : null;
                UpdateStatus();
            }
            Video_TextBox.SelectionStart = Video_TextBox.Text.Length;
        }

        private void Sub_SelectFileBtn_Click(object sender, EventArgs e)
        {
            var filename = OpenFileSelectDialog(SubExts);
            if (filename != null)
            {
                Sub_TextBox.Text = filename;
                vsItem.Sub = !string.IsNullOrWhiteSpace(filename) ? filename : null;
                UpdateStatus();
            }
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

        /*private void Save()
        {
            try
            {
                _Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        private void UpdateStatus()
        {
            if (string.IsNullOrWhiteSpace(vsItem.MatchKey))
                vsItem.Status = VsStatus.Unmatched;

            if (vsItem.Video != null)
                vsItem.Status = VsStatus.SubLack;

            if (vsItem.Sub != null)
                vsItem.Status = VsStatus.VideoLack;

            if (vsItem.Video != null && vsItem.Sub != null)
                vsItem.Status = VsStatus.Ready;
        }

        private void Video_ClearBtn_Click(object sender, EventArgs e)
        {
            vsItem.Video = null;
            Video_TextBox.Text = "";
        }

        private void Sub_ClearBtn_Click(object sender, EventArgs e)
        {
            vsItem.Sub = null;
            Sub_TextBox.Text = "";
        }

        private void AddItemBtn_Click(object sender, EventArgs e)
        {
            CreateItem();
            mainForm.RefreshFileListUi(removeNull: false);
        }

        private void CreateItem()
        {
            vsItem = new VsItem();
            vsList.Add(vsItem);
            RefreshByVsItem();
        }

        private void RemoveItemBtn_Click(object sender, EventArgs e)
        {
            if (AppSettings.ListItemRemovePrompt)
            {
                var result = MessageBox.Show($"你要删除当前编辑的项目吗？", "删除编辑项", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            int pos = vsList.IndexOf(vsItem);
            if (pos < 0) return;
            vsList.Remove(vsItem);
            if (vsList.Count > 0)
            {
                if (pos < vsList.Count)
                    vsItem = vsList[pos];
                else
                    vsItem = vsList[pos-1];
                RefreshByVsItem();
            }
            else
            {
                CreateItem();
            }
            mainForm.RefreshFileListUi(removeNull: false);
        }

        private void PrevItemBtn_Click(object sender, EventArgs e)
        {
            int pos = vsList.IndexOf(vsItem) - 1;
            if (pos < 0) return;
            mainForm.RefreshFileListUi(removeNull: false);
            vsItem = vsList[pos];
            RefreshByVsItem();
        }

        private void NextItemBtn_Click(object sender, EventArgs e)
        {
            int pos = vsList.IndexOf(vsItem) + 1;
            if (pos >= vsList.Count) return;
            mainForm.RefreshFileListUi(removeNull: false);
            vsItem = vsList[pos];
            RefreshByVsItem();
        }

        private void VsItemEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.RefreshFileListUi();
        }
    }
}
