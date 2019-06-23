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
            this.PathSelBtn = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.FileListBox = new System.Windows.Forms.ListBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.MatchEpisodeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PathSelBtn
            // 
            this.PathSelBtn.Location = new System.Drawing.Point(12, 12);
            this.PathSelBtn.Name = "PathSelBtn";
            this.PathSelBtn.Size = new System.Drawing.Size(96, 23);
            this.PathSelBtn.TabIndex = 0;
            this.PathSelBtn.Text = "打开文件夹";
            this.PathSelBtn.UseVisualStyleBackColor = true;
            this.PathSelBtn.Click += new System.EventHandler(this.PathSelBtn_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(114, 13);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.ReadOnly = true;
            this.PathTextBox.Size = new System.Drawing.Size(490, 21);
            this.PathTextBox.TabIndex = 1;
            // 
            // FileListBox
            // 
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.HorizontalScrollbar = true;
            this.FileListBox.ItemHeight = 12;
            this.FileListBox.Location = new System.Drawing.Point(12, 50);
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.Size = new System.Drawing.Size(592, 352);
            this.FileListBox.TabIndex = 2;
            // 
            // StartBtn
            // 
            this.StartBtn.Enabled = false;
            this.StartBtn.Location = new System.Drawing.Point(166, 415);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(135, 23);
            this.StartBtn.TabIndex = 3;
            this.StartBtn.Text = "2. 执行重命名";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // MatchEpisodeBtn
            // 
            this.MatchEpisodeBtn.Enabled = false;
            this.MatchEpisodeBtn.Location = new System.Drawing.Point(12, 415);
            this.MatchEpisodeBtn.Name = "MatchEpisodeBtn";
            this.MatchEpisodeBtn.Size = new System.Drawing.Size(135, 23);
            this.MatchEpisodeBtn.TabIndex = 4;
            this.MatchEpisodeBtn.Text = "1. 自动匹配集数";
            this.MatchEpisodeBtn.UseVisualStyleBackColor = true;
            this.MatchEpisodeBtn.Click += new System.EventHandler(this.MatchEpisodeBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(619, 450);
            this.Controls.Add(this.MatchEpisodeBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.FileListBox);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.PathSelBtn);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字幕文件批量重命名 - qwqaq.com";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PathSelBtn;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.ListBox FileListBox;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button MatchEpisodeBtn;
    }
}

