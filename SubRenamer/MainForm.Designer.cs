namespace SubRenamer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StartBtn = new System.Windows.Forms.Button();
            this.CopyrightText = new System.Windows.Forms.Label();
            this.FileListUi = new System.Windows.Forms.ListView();
            this.MatchKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Video = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Subtitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PreviewCheckBox = new System.Windows.Forms.CheckBox();
            this.MainContPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.R_RuleBtn = new System.Windows.Forms.Button();
            this.R_ClearAllBtn = new System.Windows.Forms.Button();
            this.R_ReMatchBtn = new System.Windows.Forms.Button();
            this.R_RemoveBtn = new System.Windows.Forms.Button();
            this.R_EditBtn = new System.Windows.Forms.Button();
            this.R_OpenFolderBtn = new System.Windows.Forms.Button();
            this.R_OpenFileBtn = new System.Windows.Forms.Button();
            this.R_SettingBtn = new System.Windows.Forms.Button();
            this.MainContWrapPanel = new System.Windows.Forms.Panel();
            this.TopMenu = new System.Windows.Forms.MainMenu(this.components);
            this.TopMenu_File = new System.Windows.Forms.MenuItem();
            this.TopMenu_OpenFileBtn = new System.Windows.Forms.MenuItem();
            this.TopMenu_OpenFolderBtn = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.TopMenu_ReMatch = new System.Windows.Forms.MenuItem();
            this.TopMenu_ClearAll = new System.Windows.Forms.MenuItem();
            this.TopMenu_Rule = new System.Windows.Forms.MenuItem();
            this.TopMenu_Setting = new System.Windows.Forms.MenuItem();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MainContPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.MainContWrapPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartBtn.Location = new System.Drawing.Point(0, 752);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(0);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(190, 74);
            this.StartBtn.TabIndex = 8;
            this.StartBtn.Text = "一键改名";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // CopyrightText
            // 
            this.CopyrightText.AutoSize = true;
            this.CopyrightText.BackColor = System.Drawing.Color.Transparent;
            this.CopyrightText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyrightText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CopyrightText.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.CopyrightText.ForeColor = System.Drawing.Color.DarkGray;
            this.CopyrightText.Location = new System.Drawing.Point(7, 684);
            this.CopyrightText.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
            this.CopyrightText.Name = "CopyrightText";
            this.CopyrightText.Size = new System.Drawing.Size(176, 16);
            this.CopyrightText.TabIndex = 10;
            this.CopyrightText.Text = "(c) qwqaq.com";
            this.CopyrightText.Click += new System.EventHandler(this.CopyrightText_Click);
            // 
            // FileListUi
            // 
            this.FileListUi.AllowDrop = true;
            this.FileListUi.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MatchKey,
            this.Video,
            this.Subtitle,
            this.Status});
            this.FileListUi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListUi.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileListUi.FullRowSelect = true;
            this.FileListUi.HideSelection = false;
            this.FileListUi.Location = new System.Drawing.Point(0, 8);
            this.FileListUi.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.FileListUi.Name = "FileListUi";
            this.FileListUi.ShowItemToolTips = true;
            this.FileListUi.Size = new System.Drawing.Size(1272, 826);
            this.FileListUi.TabIndex = 16;
            this.FileListUi.UseCompatibleStateImageBehavior = false;
            this.FileListUi.View = System.Windows.Forms.View.Details;
            this.FileListUi.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileListUi_DragDrop);
            this.FileListUi.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileListUi_DragEnter);
            this.FileListUi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileListUi_KeyDown);
            this.FileListUi.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FileListUi_MouseClick);
            // 
            // MatchKey
            // 
            this.MatchKey.Text = "匹配";
            this.MatchKey.Width = 80;
            // 
            // Video
            // 
            this.Video.Text = "视频";
            this.Video.Width = 520;
            // 
            // Subtitle
            // 
            this.Subtitle.Text = "字幕";
            this.Subtitle.Width = 520;
            // 
            // Status
            // 
            this.Status.Text = "状态";
            this.Status.Width = 120;
            // 
            // PreviewCheckBox
            // 
            this.PreviewCheckBox.AutoSize = true;
            this.PreviewCheckBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PreviewCheckBox.Location = new System.Drawing.Point(3, 728);
            this.PreviewCheckBox.Name = "PreviewCheckBox";
            this.PreviewCheckBox.Size = new System.Drawing.Size(184, 21);
            this.PreviewCheckBox.TabIndex = 17;
            this.PreviewCheckBox.Text = "改名预览";
            this.PreviewCheckBox.UseVisualStyleBackColor = true;
            this.PreviewCheckBox.CheckedChanged += new System.EventHandler(this.PreviewCheckBox_CheckedChanged);
            // 
            // MainContPanel
            // 
            this.MainContPanel.ColumnCount = 2;
            this.MainContPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainContPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.MainContPanel.Controls.Add(this.FileListUi, 0, 1);
            this.MainContPanel.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.MainContPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContPanel.Location = new System.Drawing.Point(10, 0);
            this.MainContPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainContPanel.Name = "MainContPanel";
            this.MainContPanel.RowCount = 2;
            this.MainContPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.MainContPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainContPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainContPanel.Size = new System.Drawing.Size(1467, 834);
            this.MainContPanel.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.R_RuleBtn, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.R_ClearAllBtn, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.R_ReMatchBtn, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.CopyrightText, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this.R_RemoveBtn, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.R_EditBtn, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.R_OpenFolderBtn, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.PreviewCheckBox, 0, 13);
            this.tableLayoutPanel2.Controls.Add(this.StartBtn, 0, 14);
            this.tableLayoutPanel2.Controls.Add(this.R_OpenFileBtn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.R_SettingBtn, 0, 10);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1277, 8);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 15;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.11475F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.88525F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(190, 826);
            this.tableLayoutPanel2.TabIndex = 18;
            // 
            // R_RuleBtn
            // 
            this.R_RuleBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_RuleBtn.Location = new System.Drawing.Point(0, 370);
            this.R_RuleBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_RuleBtn.Name = "R_RuleBtn";
            this.R_RuleBtn.Size = new System.Drawing.Size(190, 47);
            this.R_RuleBtn.TabIndex = 31;
            this.R_RuleBtn.Text = "规则";
            this.R_RuleBtn.UseVisualStyleBackColor = true;
            this.R_RuleBtn.Click += new System.EventHandler(this.R_RuleBtn_Click);
            // 
            // R_ClearAllBtn
            // 
            this.R_ClearAllBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_ClearAllBtn.Location = new System.Drawing.Point(0, 300);
            this.R_ClearAllBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_ClearAllBtn.Name = "R_ClearAllBtn";
            this.R_ClearAllBtn.Size = new System.Drawing.Size(190, 47);
            this.R_ClearAllBtn.TabIndex = 26;
            this.R_ClearAllBtn.Text = "清空列表";
            this.R_ClearAllBtn.UseVisualStyleBackColor = true;
            this.R_ClearAllBtn.Click += new System.EventHandler(this.R_ClearAllBtn_Click);
            // 
            // R_ReMatchBtn
            // 
            this.R_ReMatchBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_ReMatchBtn.Location = new System.Drawing.Point(0, 250);
            this.R_ReMatchBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_ReMatchBtn.Name = "R_ReMatchBtn";
            this.R_ReMatchBtn.Size = new System.Drawing.Size(190, 47);
            this.R_ReMatchBtn.TabIndex = 25;
            this.R_ReMatchBtn.Text = "重新匹配";
            this.R_ReMatchBtn.UseVisualStyleBackColor = true;
            this.R_ReMatchBtn.Click += new System.EventHandler(this.R_ReMatchBtn_Click);
            // 
            // R_RemoveBtn
            // 
            this.R_RemoveBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_RemoveBtn.Location = new System.Drawing.Point(0, 180);
            this.R_RemoveBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_RemoveBtn.Name = "R_RemoveBtn";
            this.R_RemoveBtn.Size = new System.Drawing.Size(190, 47);
            this.R_RemoveBtn.TabIndex = 22;
            this.R_RemoveBtn.Text = "删除";
            this.R_RemoveBtn.UseVisualStyleBackColor = true;
            this.R_RemoveBtn.Click += new System.EventHandler(this.R_RemoveBtn_Click);
            // 
            // R_EditBtn
            // 
            this.R_EditBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_EditBtn.Location = new System.Drawing.Point(0, 130);
            this.R_EditBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_EditBtn.Name = "R_EditBtn";
            this.R_EditBtn.Size = new System.Drawing.Size(190, 47);
            this.R_EditBtn.TabIndex = 20;
            this.R_EditBtn.Text = "编辑";
            this.R_EditBtn.UseVisualStyleBackColor = true;
            this.R_EditBtn.Click += new System.EventHandler(this.R_EditBtn_Click);
            // 
            // R_OpenFolderBtn
            // 
            this.R_OpenFolderBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_OpenFolderBtn.Location = new System.Drawing.Point(0, 50);
            this.R_OpenFolderBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_OpenFolderBtn.Name = "R_OpenFolderBtn";
            this.R_OpenFolderBtn.Size = new System.Drawing.Size(190, 47);
            this.R_OpenFolderBtn.TabIndex = 19;
            this.R_OpenFolderBtn.Text = "导入文件夹";
            this.R_OpenFolderBtn.UseVisualStyleBackColor = true;
            this.R_OpenFolderBtn.Click += new System.EventHandler(this.R_OpenFolderBtn_Click);
            // 
            // R_OpenFileBtn
            // 
            this.R_OpenFileBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_OpenFileBtn.Location = new System.Drawing.Point(0, 0);
            this.R_OpenFileBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_OpenFileBtn.Name = "R_OpenFileBtn";
            this.R_OpenFileBtn.Size = new System.Drawing.Size(190, 47);
            this.R_OpenFileBtn.TabIndex = 18;
            this.R_OpenFileBtn.Text = "导入文件";
            this.R_OpenFileBtn.UseVisualStyleBackColor = true;
            this.R_OpenFileBtn.Click += new System.EventHandler(this.R_OpenFileBtn_Click);
            // 
            // R_SettingBtn
            // 
            this.R_SettingBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.R_SettingBtn.Location = new System.Drawing.Point(0, 420);
            this.R_SettingBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.R_SettingBtn.Name = "R_SettingBtn";
            this.R_SettingBtn.Size = new System.Drawing.Size(190, 47);
            this.R_SettingBtn.TabIndex = 33;
            this.R_SettingBtn.Text = "设置";
            this.R_SettingBtn.UseVisualStyleBackColor = true;
            this.R_SettingBtn.Click += new System.EventHandler(this.R_SettingBtn_Click);
            // 
            // MainContWrapPanel
            // 
            this.MainContWrapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainContWrapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainContWrapPanel.Controls.Add(this.MainContPanel);
            this.MainContWrapPanel.Location = new System.Drawing.Point(-2, 0);
            this.MainContWrapPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainContWrapPanel.Name = "MainContWrapPanel";
            this.MainContWrapPanel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.MainContWrapPanel.Size = new System.Drawing.Size(1489, 846);
            this.MainContWrapPanel.TabIndex = 20;
            // 
            // TopMenu
            // 
            this.TopMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.TopMenu_File,
            this.TopMenu_Rule,
            this.TopMenu_Setting});
            // 
            // TopMenu_File
            // 
            this.TopMenu_File.Index = 0;
            this.TopMenu_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.TopMenu_OpenFileBtn,
            this.TopMenu_OpenFolderBtn,
            this.menuItem1,
            this.TopMenu_ReMatch,
            this.TopMenu_ClearAll});
            this.TopMenu_File.Text = "文件";
            // 
            // TopMenu_OpenFileBtn
            // 
            this.TopMenu_OpenFileBtn.Index = 0;
            this.TopMenu_OpenFileBtn.Text = "导入文件...";
            this.TopMenu_OpenFileBtn.Click += new System.EventHandler(this.TopMenu_OpenFileBtn_Click);
            // 
            // TopMenu_OpenFolderBtn
            // 
            this.TopMenu_OpenFolderBtn.Index = 1;
            this.TopMenu_OpenFolderBtn.Text = "导入文件夹...";
            this.TopMenu_OpenFolderBtn.Click += new System.EventHandler(this.TopMenu_OpenFolderBtn_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // TopMenu_ReMatch
            // 
            this.TopMenu_ReMatch.Index = 3;
            this.TopMenu_ReMatch.Text = "重新匹配";
            this.TopMenu_ReMatch.Click += new System.EventHandler(this.TopMenu_ReMatch_Click);
            // 
            // TopMenu_ClearAll
            // 
            this.TopMenu_ClearAll.Index = 4;
            this.TopMenu_ClearAll.Text = "清空列表";
            this.TopMenu_ClearAll.Click += new System.EventHandler(this.TopMenu_ClearAll_Click);
            // 
            // TopMenu_Rule
            // 
            this.TopMenu_Rule.Index = 1;
            this.TopMenu_Rule.Text = "规则";
            this.TopMenu_Rule.Click += new System.EventHandler(this.TopMenu_Rule_Click);
            // 
            // TopMenu_Setting
            // 
            this.TopMenu_Setting.Index = 2;
            this.TopMenu_Setting.Text = "设置";
            this.TopMenu_Setting.Click += new System.EventHandler(this.TopMenu_Setting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 844);
            this.Controls.Add(this.MainContWrapPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Menu = this.TopMenu;
            this.MinimumSize = new System.Drawing.Size(1500, 882);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字幕文件批量改名 (SubRenamer)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.MainContPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.MainContWrapPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Label CopyrightText;
        private System.Windows.Forms.ListView FileListUi;
        private System.Windows.Forms.ColumnHeader MatchKey;
        private System.Windows.Forms.ColumnHeader Video;
        private System.Windows.Forms.ColumnHeader Subtitle;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.CheckBox PreviewCheckBox;
        private System.Windows.Forms.TableLayoutPanel MainContPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel MainContWrapPanel;
        private System.Windows.Forms.MainMenu TopMenu;
        private System.Windows.Forms.MenuItem TopMenu_File;
        private System.Windows.Forms.MenuItem TopMenu_Setting;
        private System.Windows.Forms.MenuItem TopMenu_OpenFileBtn;
        private System.Windows.Forms.MenuItem TopMenu_OpenFolderBtn;
        private System.Windows.Forms.Button R_OpenFolderBtn;
        private System.Windows.Forms.Button R_OpenFileBtn;
        private System.Windows.Forms.Button R_EditBtn;
        private System.Windows.Forms.Button R_RemoveBtn;
        private System.Windows.Forms.Button R_ReMatchBtn;
        private System.Windows.Forms.Button R_ClearAllBtn;
        private System.Windows.Forms.Button R_RuleBtn;
        private System.Windows.Forms.Button R_SettingBtn;
        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.MenuItem TopMenu_Rule;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem TopMenu_ReMatch;
        private System.Windows.Forms.MenuItem TopMenu_ClearAll;
    }
}

