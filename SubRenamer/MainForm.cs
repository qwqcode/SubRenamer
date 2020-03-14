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

namespace SubRenamer
{
    public partial class MainForm : Form
    {
        private static SettingForm SettingForm;

        public MainForm()
        {
            InitializeComponent();
            Text += $" {Program.GetVersionStr()}"; // 追加版本号
            SettingForm = new SettingForm(this); // 设置窗体

            InitShorcut(); // 初始化快捷键
        }
        
        // 窗口加载完后
        private void MainForm_Load(object sender, EventArgs e)
        {
            /*LoadFilesByPath(Application.StartupPath);*/
            SetWindowTheme(this.FileListUi.Handle, "Explorer", null);
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
                    foreach (var fileName in fbd.FileNames)
                    {
                        var file = new FileInfo(fileName);

                        // 视频文件
                        if (VideoExts.Contains(file.Extension.ToString().ToLower()))
                            VideoFileList.Add(file);

                        // 字幕文件
                        else if (SubExts.Contains(file.Extension.ToString().ToLower()))
                            SubFileList.Add(file);
                    }

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
                    foreach (var folderPath in fbd.FileNames)
                    {
                        var folder = new DirectoryInfo(folderPath);
                        var files = folder.GetFiles("*");

                        // 视频文件
                        foreach (var file in files.Where(s => VideoExts.Contains(s.Extension.ToString().ToLower())))
                            VideoFileList.Add(file);

                        // 字幕文件
                        foreach (var file in files.Where(s => SubExts.Contains(s.Extension.ToString().ToLower())))
                            SubFileList.Add(file);
                    }

                    MatchVideoSub();
                }
            }
        }

        // 删除选定的项目
        private void RemoveListSelectedItems()
        {
            if (FileListUi.SelectedItems.Count <= 0) return;

            if (MainSettings.Default.ListItemRemovePrompt)
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
                if (vsItem.VideoFile != null)
                    VideoFileList.RemoveAll(o => o.FullName == vsItem.VideoFile.FullName);
                if (vsItem.SubFile != null)
                    SubFileList.RemoveAll(o => o.FullName == vsItem.SubFile.FullName);
                VsList.Remove(vsItem);
                item.Remove();
            }
            MatchVideoSub();
        }

        // 全选操作
        private void SelectListAll()
        {
            foreach (ListViewItem item in FileListUi.Items)
                item.Selected = true;
        }

        // 清空列表
        private void ClearListAll()
        {
            if (VideoFileList.Count() == 0 && SubFileList.Count() == 0
                && VsList.Count() == 0 && FileListUi.Items.Count == 0)
                return;

            if (MainSettings.Default.ListItemRemovePrompt)
            {
                var result = MessageBox.Show("你要清空列表吗？", "清空列表", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            FileListUi.Items.Clear();
            VideoFileList.Clear();
            SubFileList.Clear();
            VsList.Clear();

            RefreshFileListUi();
        }

        private void ReMatch()
        {
            MatchVideoSub();
        }

        private void OpenVsItemEditor(VsItem vsItem)
        {
            var form = new VsItemEditor(this, vsItem);
            form.ShowDialog();
        }

        private void EditListSelectedItems()
        {
            if (FileListUi.SelectedItems.Count > 0)
            {
                OpenVsItemEditor((VsItem)FileListUi.SelectedItems[0].Tag);
            }
        }
        #endregion

        #region 各种操作
        private void FileListUi_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (FileListUi.FocusedItem != null
                    && FileListUi.Bounds.Contains(e.Location) == true)
                {
                    ContextMenu m = new ContextMenu();
                    var editBtn = new MenuItem("编辑");
                    editBtn.Shortcut = Shortcut.F3;
                    editBtn.Click += delegate (object sender2, EventArgs e2) {
                        EditListSelectedItems();
                    };
                    m.MenuItems.Add(editBtn);

                    var cashMenuItem2 = new MenuItem("-");
                    m.MenuItems.Add(cashMenuItem2);

                    var delBtn = new MenuItem("删除");
                    delBtn.Shortcut = Shortcut.Del;
                    delBtn.Click += delegate (object sender2, EventArgs e2) {
                        RemoveListSelectedItems();
                    };
                    m.MenuItems.Add(delBtn);
                    m.MenuItems.Add("-");

                    var selectAll = new MenuItem("全部选择");
                    selectAll.Shortcut = Shortcut.CtrlA;
                    selectAll.Click += delegate (object sender2, EventArgs e2) {
                        SelectListAll();
                    };
                    m.MenuItems.Add(selectAll);

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

        private void FileListUi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveListSelectedItems();
            else if (e.KeyCode == Keys.A && e.Control)
                SelectListAll();
        }

        // 显示预览 勾选
        private void PreviewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PreviewCheckBox.Checked)
            {
                Video.Text = "字幕修改前";
                Subtitle.Text = "字幕修改后";
            }
            else
            {
                Video.Text = "视频";
                Subtitle.Text = "字幕";
            }
            RefreshFileListUi();
        }
        #endregion

        #region 点击事件
        private void TopMenu_OpenFileBtn_Click(object sender, EventArgs e) => OpenFile();
        private void R_OpenFileBtn_Click(object sender, EventArgs e) => OpenFile();
        private void TopMenu_OpenFolderBtn_Click(object sender, EventArgs e) => OpenFolder();
        private void R_OpenFolderBtn_Click(object sender, EventArgs e) => OpenFolder();
        private void TopMenu_Setting_Click(object sender, EventArgs e) => SettingForm.ShowDialog();
        private void StartBtn_Click(object sender, EventArgs e) => StartRename();
        private void R_EditBtn_Click(object sender, EventArgs e) => EditListSelectedItems();
        private void R_RemoveBtn_Click(object sender, EventArgs e) => RemoveListSelectedItems();
        private void R_ReMatchBtn_Click(object sender, EventArgs e) => ReMatch();
        private void R_ClearAllBtn_Click(object sender, EventArgs e) => ClearListAll();
        private void R_RuleBtn_Click(object sender, EventArgs e) { }
        private void R_SettingBtn_Click(object sender, EventArgs e) => SettingForm.ShowDialog();
        private void CopyrightText_Click(object sender, EventArgs e) => Process.Start("https://qwqaq.com/?from=SubRenamer");
        #endregion

        #region 快捷键
        // 快捷键操作
        const Keys OPEN_FILE_KEY = Keys.Control | Keys.O;
        const Keys OPEN_FOLDER_KEY = Keys.Control | Keys.Shift | Keys.O;

        // 初始化快捷键
        private void InitShorcut()
        {
            // 快捷键显示
            TopMenu_OpenFileBtn.Shortcut = (Shortcut)OPEN_FILE_KEY;
            TopMenu_OpenFolderBtn.Shortcut = (Shortcut)OPEN_FOLDER_KEY;
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

            return base.ProcessCmdKey(ref msg, keys);
        }
        #endregion

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
    }
}
