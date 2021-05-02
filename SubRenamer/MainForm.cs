using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using static SubRenamer.Global;

namespace SubRenamer
{
    public partial class MainForm : Form
    {
        private static SettingForm SettingForm;

        public MainForm()
        {
            InitializeComponent();

            UpdateWinTitle();
            SettingForm = new SettingForm(this); // 设置窗体

            InitShorcut(); // 初始化快捷键
        }
        
        // 窗口加载完后
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetWindowTheme(this.FileListUi.Handle, "Explorer", null);
        }

        private void UpdateWinTitle()
        {
            if (!AppSettings.RenameVideo)
            {
                Text = "字幕文件批量改名 (SubRenamer)";
            }
            else
            {
                Text = "视频文件批量改名 (VideoRenamer)";
            }
            Text += $" {Program.GetVersionStr()}"; // 追加版本号
        }

        #region 各种操作
        // 导入 文件
        private void OpenFile()
        {
            using (var fbd = new CommonOpenFileDialog()
            {
                Multiselect = true,
            })
            {
                fbd.Filters.Add(new CommonFileDialogFilter("视频或字幕文件", string.Join(";", VideoExts.Concat(SubExts).ToList())));
                var result = fbd.ShowDialog();

                if (result == CommonFileDialogResult.Ok && fbd.FileNames.Count() > 0)
                {
                    SwitchPreview(false);

                    foreach (var fileName in fbd.FileNames) FileListAdd(new FileInfo(fileName));

                    MatchVideoSub();
                }
            }
        }

        // 导入 文件夹
        private void OpenFolder()
        {
            using (var fbd = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Multiselect = true
            })
            {
                var result = fbd.ShowDialog();

                if (result == CommonFileDialogResult.Ok && fbd.FileNames.Count() > 0)
                {
                    SwitchPreview(false);

                    foreach (var folderPath in fbd.FileNames)
                    {
                        var folder = new DirectoryInfo(folderPath);
                        var files = folder.GetFiles("*");

                        // 添加所有 视频/字幕 文件
                        foreach (var file in files) FileListAdd(file);
                    }

                    MatchVideoSub();
                }
            }
        }

        // 文件添加
        private void FileListAdd(FileInfo file)
        {
            AppFileType fileType;
            if (VideoExts.Contains(file.Extension.ToString().ToLower()))
                fileType = AppFileType.Video;
            else if (SubExts.Contains(file.Extension.ToString().ToLower()))
                fileType = AppFileType.Sub;
            else return;

            var vsItem = new VsItem();
            if (fileType == AppFileType.Video)
            {
                if (VsList.Exists(o => o.Video == file.FullName)) return; // 重名排除
                vsItem.Video = file.FullName;
            }
            else if (fileType == AppFileType.Sub)
            {
                if (VsList.Exists(o => o.Sub == file.FullName)) return;
                vsItem.Sub = file.FullName;
            }

            vsItem.Status = VsStatus.Unmatched;
            VsList.Add(vsItem);
        }

        // 全选操作
        private void SelectListAll()
        {
            foreach (ListViewItem item in FileListUi.Items)
                item.Selected = true;
        }

        // 重新匹配
        private void ReMatch()
        {
            MatchVideoSub();
        }

        // 打开 VsItem 编辑器
        private void OpenVsItemEditor(VsItem vsItem)
        {
            var form = new VsItemEditor(this, VsList, vsItem);
            form.ShowDialog();
        }

        // 打开规则编辑器
        private void OpenRuleEditor()
        {
            var form = new RuleEditor(this);
            form.ShowDialog();
        }

        private void EditListSelectedItems()
        {
            if (FileListUi.SelectedItems.Count > 0)
            {
                OpenVsItemEditor((VsItem)FileListUi.SelectedItems[0].Tag);
            }
        }

        // 拖拽文件 Enter
        private void FileListUi_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) // 拖拽数据是否为文件
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        // 拖拽文件 Drop
        private void FileListUi_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var fileName in files) FileListAdd(new FileInfo(fileName));
            MatchVideoSub();
        }

        private void FileListUi_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (FileListUi.FocusedItem != null
                    && FileListUi.Bounds.Contains(e.Location) == true)
                {
                    var selectedItem = (VsItem)FileListUi.SelectedItems[0].Tag;
                    ContextMenu m = new ContextMenu();
                    var items = m.MenuItems;
                    items.Add(new MenuItem("编辑", (e1, s1) => EditListSelectedItems(), Shortcut.F3));
                    items.Add("-");
                    items.Add(new MenuItem("删除", (e1, s1) => RemoveListSelectedItems(), Shortcut.Del));
                    var discardVideo = new MenuItem("丢弃视频", (e1, s1) => DiscardListSelectedItemsFile(AppFileType.Video));
                    var discardSub = new MenuItem("丢弃字幕", (e1, s1) => DiscardListSelectedItemsFile(AppFileType.Sub));
                    if (string.IsNullOrWhiteSpace(selectedItem.Video)) discardVideo.Enabled = false;
                    if (string.IsNullOrWhiteSpace(selectedItem.Sub)) discardSub.Enabled = false;
                    items.Add(discardVideo);
                    items.Add(discardSub);
                    items.Add("-");
                    items.Add(new MenuItem("全选", (e1, s1) => SelectListAll(), Shortcut.CtrlA));
                    items.Add("-");
                    var openVideoFolder = new MenuItem("打开视频文件夹", (e1, s1) => OpenExplorerFile(selectedItem.Video));
                    var openSubFolder = new MenuItem("打开字幕文件夹", (e1, s1) => OpenExplorerFile(selectedItem.Sub));
                    if (string.IsNullOrWhiteSpace(selectedItem.Video)) openVideoFolder.Enabled = false;
                    if (string.IsNullOrWhiteSpace(selectedItem.Sub)) openSubFolder.Enabled = false;
                    items.Add(openVideoFolder);
                    items.Add(openSubFolder);
                    items.Add("-");
                    items.Add(new MenuItem("复制改名命令至剪切板", (e1, s1) => CopyRenameCommand()));
                    m.Show(FileListUi, new Point(e.X, e.Y));
                }
            }
        }

        // 主窗体尺寸发生变化
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // 自适应文件列表字段宽度
            int calcPathInfoWidth = (FileListUi.Width - (FileListUi.Columns[0].Width + FileListUi.Columns[3].Width + 8)) / 2;
            FileListUi.Columns[1].Width = calcPathInfoWidth;
            FileListUi.Columns[2].Width = calcPathInfoWidth;
        }

        // 显示预览 勾选
        private void PreviewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SwitchPreview(PreviewCheckBox.Checked, true);
        }

        private void SwitchPreview(bool value, bool force = false)
        {
            if (!force && value == PreviewCheckBox.Checked) return;

            PreviewCheckBox.Checked = value;
            RefreshFileListUi();

        }

        // 清空列表
        private void ClearListAll()
        {
            if (VsList.Count() == 0 && FileListUi.Items.Count == 0)
                return;

            if (AppSettings.ListItemRemovePrompt)
            {
                var result = MessageBox.Show("你要清空列表吗？", "清空列表", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            FileListUi.Items.Clear();
            VsList.Clear();

            RefreshFileListUi();
        }

        private void DiscardListSelectedItemsFile(AppFileType FileType)
        {
            if (FileListUi.SelectedItems.Count <= 0) return;

            string label = "";
            if (FileType.Equals(AppFileType.Video)) label = "视频";
            else if (FileType.Equals(AppFileType.Sub)) label = "字幕";

            if (AppSettings.ListItemRemovePrompt)
            {
                var result = MessageBox.Show($"你要丢弃选定项目的{label}吗？源文件不会被删除", $"丢弃所选项的{label}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            foreach (ListViewItem item in FileListUi.SelectedItems)
            {
                if (item.Tag == null)
                {
                    item.Remove();
                    continue;
                }

                var vsItem = (VsItem)item.Tag;
                if (FileType.Equals(AppFileType.Video)) vsItem.Video = null;
                if (FileType.Equals(AppFileType.Sub)) vsItem.Sub = null;
            }

            RefreshFileListUi();
        }

        // 删除选定的项目
        private void RemoveListSelectedItems()
        {
            if (FileListUi.SelectedItems.Count <= 0) return;

            if (AppSettings.ListItemRemovePrompt)
            {
                var result = MessageBox.Show("你要删除选定的项目吗？", "删除所选项", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            foreach (ListViewItem item in FileListUi.SelectedItems)
            {
                if (item.Tag == null)
                {
                    item.Remove();
                    continue;
                }

                var vsItem = (VsItem)item.Tag;
                VsList.Remove(vsItem);
                item.Remove();
            }

            RefreshFileListUi();
        }

        private void CopyRenameCommand()
        {
            var cmd = "";
            var renameDict = GetSubRenameDict();
            if (renameDict.Count < 0) return;
            var selectedList = new List<ListViewItem>(FileListUi.SelectedItems.OfType<ListViewItem>());
            foreach (var rename in renameDict)
            {
                if (FileListUi.SelectedItems.Count > 0
                    && !selectedList.Exists(o => o.Tag != null && ((VsItem)o.Tag).Sub == rename.Key)) continue;
                cmd += $"mv \"{rename.Key}\" \"{rename.Value}\"{Environment.NewLine}";
            }
            Clipboard.SetDataObject(cmd.Trim());
        }
        #endregion

        #region 点击事件
        private void TopMenu_OpenFileBtn_Click(object sender, EventArgs e) => OpenFile();
        private void R_OpenFileBtn_Click(object sender, EventArgs e) => OpenFile();
        private void TopMenu_OpenFolderBtn_Click(object sender, EventArgs e) => OpenFolder();
        private void R_OpenFolderBtn_Click(object sender, EventArgs e) => OpenFolder();
        private void TopMenu_Rule_Click(object sender, EventArgs e) => OpenRuleEditor();
        private void TopMenu_Setting_Click(object sender, EventArgs e) => SettingForm.ShowDialog();
        private void TopMenu_ReMatch_Click(object sender, EventArgs e) => ReMatch();
        private void TopMenu_ClearAll_Click(object sender, EventArgs e) => ClearListAll();
        private void StartBtn_Click(object sender, EventArgs e) => StartRename();
        private void R_EditBtn_Click(object sender, EventArgs e) => EditListSelectedItems();
        private void R_RemoveBtn_Click(object sender, EventArgs e) => RemoveListSelectedItems();
        private void R_ReMatchBtn_Click(object sender, EventArgs e) => ReMatch();
        private void R_ClearAllBtn_Click(object sender, EventArgs e) => ClearListAll();
        private void R_RuleBtn_Click(object sender, EventArgs e) => OpenRuleEditor();
        private void R_SettingBtn_Click(object sender, EventArgs e) => SettingForm.ShowDialog();
        private void CopyrightText_Click(object sender, EventArgs e) => Program.OpenAuthorBlog();
        #endregion

        #region 快捷键
        // 快捷键操作
        const Keys OPEN_FILE_KEY = Keys.Control | Keys.O;
        const Keys OPEN_FOLDER_KEY = Keys.Control | Keys.Shift | Keys.O;
        const Keys RE_MATCH_KEY = Keys.Control | Keys.R;
        const Keys CLEAR_ALL_KEY = Keys.Control | Keys.N;

        // 初始化快捷键
        private void InitShorcut()
        {
            // 快捷键显示
            TopMenu_OpenFileBtn.Shortcut = (Shortcut)OPEN_FILE_KEY;
            TopMenu_OpenFolderBtn.Shortcut = (Shortcut)OPEN_FOLDER_KEY;
            TopMenu_ReMatch.Shortcut = (Shortcut)RE_MATCH_KEY;
            TopMenu_ClearAll.Shortcut = (Shortcut)CLEAR_ALL_KEY;
        }

        // 快捷键操作
        protected override bool ProcessCmdKey(ref Message msg, Keys keys)
        {
            if (keys == OPEN_FILE_KEY)
            {
                TopMenu_OpenFileBtn.PerformClick();
                return true;
            }

            if (keys == OPEN_FOLDER_KEY)
            {
                TopMenu_OpenFolderBtn.PerformClick();
                return true;
            }

            if (keys == RE_MATCH_KEY)
            {
                TopMenu_ReMatch.PerformClick();
                return true;
            }

            if (keys == CLEAR_ALL_KEY)
            {
                TopMenu_ClearAll.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keys);
        }

        // 列表快捷键
        private void FileListUi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
                EditListSelectedItems();
            if (e.KeyCode == Keys.Delete)
                RemoveListSelectedItems();
            else if (e.KeyCode == Keys.A && e.Control)
                SelectListAll();
        }
        #endregion

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        public void OpenExplorerFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;
            if (!File.Exists(filePath)) return;
            string args = $"/select, \"{filePath}\"";
            Process.Start("explorer.exe", args);
        }
    }
}
