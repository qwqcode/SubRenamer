namespace SubtitleRenamer
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
            this.PathSelBtn = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.VideoFileListBox = new System.Windows.Forms.ListBox();
            this.SubtitleFileListBox = new System.Windows.Forms.ListBox();
            this.StartEasyBtn = new System.Windows.Forms.Button();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.CopyrightText = new System.Windows.Forms.Label();
            this.SettingBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PathSelBtn
            // 
            this.PathSelBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PathSelBtn.Location = new System.Drawing.Point(15, 17);
            this.PathSelBtn.Name = "PathSelBtn";
            this.PathSelBtn.Size = new System.Drawing.Size(93, 25);
            this.PathSelBtn.TabIndex = 0;
            this.PathSelBtn.Text = "打开文件夹";
            this.PathSelBtn.UseVisualStyleBackColor = true;
            this.PathSelBtn.Click += new System.EventHandler(this.PathSelBtn_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PathTextBox.Location = new System.Drawing.Point(114, 18);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.ReadOnly = true;
            this.PathTextBox.Size = new System.Drawing.Size(496, 23);
            this.PathTextBox.TabIndex = 1;
            // 
            // VideoFileListBox
            // 
            this.VideoFileListBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.VideoFileListBox.FormattingEnabled = true;
            this.VideoFileListBox.HorizontalScrollbar = true;
            this.VideoFileListBox.ItemHeight = 17;
            this.VideoFileListBox.Location = new System.Drawing.Point(441, 72);
            this.VideoFileListBox.Name = "VideoFileListBox";
            this.VideoFileListBox.Size = new System.Drawing.Size(420, 463);
            this.VideoFileListBox.TabIndex = 2;
            this.VideoFileListBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FileListBox_MouseMove);
            // 
            // SubtitleFileListBox
            // 
            this.SubtitleFileListBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SubtitleFileListBox.FormattingEnabled = true;
            this.SubtitleFileListBox.HorizontalScrollbar = true;
            this.SubtitleFileListBox.ItemHeight = 17;
            this.SubtitleFileListBox.Location = new System.Drawing.Point(12, 72);
            this.SubtitleFileListBox.Name = "SubtitleFileListBox";
            this.SubtitleFileListBox.Size = new System.Drawing.Size(420, 463);
            this.SubtitleFileListBox.TabIndex = 6;
            this.SubtitleFileListBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FileListBox_MouseMove);
            // 
            // StartEasyBtn
            // 
            this.StartEasyBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartEasyBtn.Location = new System.Drawing.Point(12, 12);
            this.StartEasyBtn.Name = "StartEasyBtn";
            this.StartEasyBtn.Size = new System.Drawing.Size(122, 47);
            this.StartEasyBtn.TabIndex = 8;
            this.StartEasyBtn.Text = "一键改名";
            this.StartEasyBtn.UseVisualStyleBackColor = true;
            this.StartEasyBtn.Click += new System.EventHandler(this.StartEasyBtn_Click);
            // 
            // CopyrightText
            // 
            this.CopyrightText.AutoSize = true;
            this.CopyrightText.BackColor = System.Drawing.Color.Transparent;
            this.CopyrightText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyrightText.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.CopyrightText.ForeColor = System.Drawing.Color.DarkGray;
            this.CopyrightText.Location = new System.Drawing.Point(777, 41);
            this.CopyrightText.Name = "CopyrightText";
            this.CopyrightText.Size = new System.Drawing.Size(87, 16);
            this.CopyrightText.TabIndex = 10;
            this.CopyrightText.Text = "(c) qwqaq.com";
            this.CopyrightText.Click += new System.EventHandler(this.CopyrightText_Click);
            // 
            // SettingBtn
            // 
            this.SettingBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SettingBtn.Location = new System.Drawing.Point(779, 12);
            this.SettingBtn.Name = "SettingBtn";
            this.SettingBtn.Size = new System.Drawing.Size(82, 24);
            this.SettingBtn.TabIndex = 13;
            this.SettingBtn.Text = "设置";
            this.SettingBtn.UseVisualStyleBackColor = true;
            this.SettingBtn.Click += new System.EventHandler(this.SettingBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PathTextBox);
            this.groupBox1.Controls.Add(this.PathSelBtn);
            this.groupBox1.Location = new System.Drawing.Point(141, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 53);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(876, 549);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SettingBtn);
            this.Controls.Add(this.StartEasyBtn);
            this.Controls.Add(this.SubtitleFileListBox);
            this.Controls.Add(this.CopyrightText);
            this.Controls.Add(this.VideoFileListBox);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字幕文件批量重命名 - qwqaq.com";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PathSelBtn;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.ListBox VideoFileListBox;
        private System.Windows.Forms.ListBox SubtitleFileListBox;
        private System.Windows.Forms.Button StartEasyBtn;
        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.Label CopyrightText;
        private System.Windows.Forms.Button SettingBtn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

