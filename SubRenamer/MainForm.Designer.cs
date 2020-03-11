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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StartBtn = new System.Windows.Forms.Button();
            this.CopyrightText = new System.Windows.Forms.Label();
            this.FileListUi = new System.Windows.Forms.ListView();
            this.MatchKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Video = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Subtitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.MainContPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuPanel = new System.Windows.Forms.Panel();
            this.MainContWrapPanel = new System.Windows.Forms.Panel();
            this.MainContPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.TopMenuStrip.SuspendLayout();
            this.TopMenuPanel.SuspendLayout();
            this.MainContWrapPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartBtn.Location = new System.Drawing.Point(0, 700);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(0);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(190, 58);
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
            this.CopyrightText.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.CopyrightText.ForeColor = System.Drawing.Color.DarkGray;
            this.CopyrightText.Location = new System.Drawing.Point(7, 10);
            this.CopyrightText.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
            this.CopyrightText.Name = "CopyrightText";
            this.CopyrightText.Size = new System.Drawing.Size(147, 25);
            this.CopyrightText.TabIndex = 10;
            this.CopyrightText.Text = "(c) qwqaq.com";
            this.CopyrightText.Click += new System.EventHandler(this.CopyrightText_Click);
            // 
            // FileListUi
            // 
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
            this.FileListUi.Size = new System.Drawing.Size(1259, 758);
            this.FileListUi.TabIndex = 16;
            this.FileListUi.UseCompatibleStateImageBehavior = false;
            this.FileListUi.View = System.Windows.Forms.View.Details;
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
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 665);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(122, 32);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "改名预览";
            this.checkBox1.UseVisualStyleBackColor = true;
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
            this.MainContPanel.Size = new System.Drawing.Size(1454, 766);
            this.MainContPanel.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.checkBox1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.StartBtn, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.CopyrightText, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1264, 8);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.576329F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.42367F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(190, 758);
            this.tableLayoutPanel2.TabIndex = 18;
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopMenuStrip.GripMargin = new System.Windows.Forms.Padding(4);
            this.TopMenuStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.SettingToolStripMenuItem});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.TopMenuStrip.Size = new System.Drawing.Size(1476, 40);
            this.TopMenuStrip.TabIndex = 19;
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileToolStripMenuItem,
            this.OpenFolderToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(88, 40);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // OpenFileToolStripMenuItem
            // 
            this.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem";
            this.OpenFileToolStripMenuItem.Size = new System.Drawing.Size(315, 40);
            this.OpenFileToolStripMenuItem.Text = "导入文件";
            // 
            // OpenFolderToolStripMenuItem
            // 
            this.OpenFolderToolStripMenuItem.Name = "OpenFolderToolStripMenuItem";
            this.OpenFolderToolStripMenuItem.Size = new System.Drawing.Size(315, 40);
            this.OpenFolderToolStripMenuItem.Text = "导入文件夹";
            this.OpenFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenFolderToolStripMenuItem_Click);
            // 
            // SettingToolStripMenuItem
            // 
            this.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem";
            this.SettingToolStripMenuItem.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.SettingToolStripMenuItem.Size = new System.Drawing.Size(88, 40);
            this.SettingToolStripMenuItem.Text = "设置";
            this.SettingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
            // 
            // TopMenuPanel
            // 
            this.TopMenuPanel.BackColor = System.Drawing.Color.White;
            this.TopMenuPanel.Controls.Add(this.TopMenuStrip);
            this.TopMenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.TopMenuPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopMenuPanel.Name = "TopMenuPanel";
            this.TopMenuPanel.Size = new System.Drawing.Size(1476, 40);
            this.TopMenuPanel.TabIndex = 19;
            // 
            // MainContWrapPanel
            // 
            this.MainContWrapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainContWrapPanel.Controls.Add(this.MainContPanel);
            this.MainContWrapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContWrapPanel.Location = new System.Drawing.Point(0, 40);
            this.MainContWrapPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainContWrapPanel.Name = "MainContWrapPanel";
            this.MainContWrapPanel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.MainContWrapPanel.Size = new System.Drawing.Size(1476, 778);
            this.MainContWrapPanel.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 818);
            this.Controls.Add(this.MainContWrapPanel);
            this.Controls.Add(this.TopMenuPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.TopMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(7);
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
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.TopMenuPanel.ResumeLayout(false);
            this.TopMenuPanel.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TableLayoutPanel MainContPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingToolStripMenuItem;
        private System.Windows.Forms.Panel TopMenuPanel;
        private System.Windows.Forms.Panel MainContWrapPanel;
    }
}

