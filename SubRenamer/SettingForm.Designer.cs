namespace SubRenamer
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VersionText = new System.Windows.Forms.Label();
            this.UpdateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.GithubLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AuthorLinkLabel = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.RawSubtitleBuckup = new System.Windows.Forms.CheckBox();
            this.ListShowFileFullName = new System.Windows.Forms.CheckBox();
            this.ListItemRemovePrompt = new System.Windows.Forms.CheckBox();
            this.BlogLabel = new System.Windows.Forms.LinkLabel();
            this.RenameVideo = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxVideoExtension = new System.Windows.Forms.ListBox();
            this.listBoxSubExtension = new System.Windows.Forms.ListBox();
            this.labelVideoExtension = new System.Windows.Forms.Label();
            this.labelSubExtension = new System.Windows.Forms.Label();
            this.btnAddVideoExtension = new System.Windows.Forms.Button();
            this.btnDeleteVideoExtension = new System.Windows.Forms.Button();
            this.btnDeleteSubExtension = new System.Windows.Forms.Button();
            this.btnAddSubExtension = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // VersionText
            // 
            this.VersionText.AutoSize = true;
            this.VersionText.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.VersionText.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VersionText.Location = new System.Drawing.Point(643, 300);
            this.VersionText.Name = "VersionText";
            this.VersionText.Size = new System.Drawing.Size(46, 16);
            this.VersionText.TabIndex = 16;
            this.VersionText.Text = "v0.0.0.0";
            // 
            // UpdateLinkLabel
            // 
            this.UpdateLinkLabel.AutoSize = true;
            this.UpdateLinkLabel.Location = new System.Drawing.Point(583, 299);
            this.UpdateLinkLabel.Name = "UpdateLinkLabel";
            this.UpdateLinkLabel.Size = new System.Drawing.Size(32, 17);
            this.UpdateLinkLabel.TabIndex = 24;
            this.UpdateLinkLabel.TabStop = true;
            this.UpdateLinkLabel.Text = "更新";
            this.UpdateLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.UpdateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateLinkLabel_LinkClicked);
            // 
            // GithubLinkLabel
            // 
            this.GithubLinkLabel.AutoSize = true;
            this.GithubLinkLabel.Location = new System.Drawing.Point(200, 299);
            this.GithubLinkLabel.Name = "GithubLinkLabel";
            this.GithubLinkLabel.Size = new System.Drawing.Size(56, 17);
            this.GithubLinkLabel.TabIndex = 23;
            this.GithubLinkLabel.TabStop = true;
            this.GithubLinkLabel.Text = "开源仓库";
            this.GithubLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.GithubLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLinkLabel_LinkClicked);
            // 
            // AuthorLinkLabel
            // 
            this.AuthorLinkLabel.AutoSize = true;
            this.AuthorLinkLabel.Location = new System.Drawing.Point(77, 299);
            this.AuthorLinkLabel.Name = "AuthorLinkLabel";
            this.AuthorLinkLabel.Size = new System.Drawing.Size(32, 17);
            this.AuthorLinkLabel.TabIndex = 22;
            this.AuthorLinkLabel.TabStop = true;
            this.AuthorLinkLabel.Text = "作者";
            this.AuthorLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.AuthorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AuthorLinkLabel_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(17, 299);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(32, 17);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "反馈";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(47, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "改名前备份到相同目录下的 SubBackup 文件夹中";
            // 
            // RawSubtitleBuckup
            // 
            this.RawSubtitleBuckup.AutoSize = true;
            this.RawSubtitleBuckup.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.RawSubtitleBuckup.Location = new System.Drawing.Point(22, 20);
            this.RawSubtitleBuckup.Name = "RawSubtitleBuckup";
            this.RawSubtitleBuckup.Size = new System.Drawing.Size(114, 20);
            this.RawSubtitleBuckup.TabIndex = 2;
            this.RawSubtitleBuckup.Text = "备份原始字幕文件";
            this.RawSubtitleBuckup.UseVisualStyleBackColor = true;
            // 
            // ListShowFileFullName
            // 
            this.ListShowFileFullName.AutoSize = true;
            this.ListShowFileFullName.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.ListShowFileFullName.Location = new System.Drawing.Point(22, 130);
            this.ListShowFileFullName.Name = "ListShowFileFullName";
            this.ListShowFileFullName.Size = new System.Drawing.Size(114, 20);
            this.ListShowFileFullName.TabIndex = 4;
            this.ListShowFileFullName.Text = "显示文件完整路径";
            this.ListShowFileFullName.UseVisualStyleBackColor = true;
            this.ListShowFileFullName.CheckedChanged += new System.EventHandler(this.ListShowFileFullNameCheckBox_CheckedChanged);
            // 
            // ListItemRemovePrompt
            // 
            this.ListItemRemovePrompt.AutoSize = true;
            this.ListItemRemovePrompt.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.ListItemRemovePrompt.Location = new System.Drawing.Point(22, 95);
            this.ListItemRemovePrompt.Name = "ListItemRemovePrompt";
            this.ListItemRemovePrompt.Size = new System.Drawing.Size(114, 20);
            this.ListItemRemovePrompt.TabIndex = 3;
            this.ListItemRemovePrompt.Text = "显示各种删除提示";
            this.ListItemRemovePrompt.UseVisualStyleBackColor = true;
            // 
            // BlogLabel
            // 
            this.BlogLabel.AutoSize = true;
            this.BlogLabel.Location = new System.Drawing.Point(137, 299);
            this.BlogLabel.Name = "BlogLabel";
            this.BlogLabel.Size = new System.Drawing.Size(35, 17);
            this.BlogLabel.TabIndex = 25;
            this.BlogLabel.TabStop = true;
            this.BlogLabel.Text = "Blog";
            this.BlogLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.BlogLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BlogLabel_LinkClicked);
            // 
            // RenameVideo
            // 
            this.RenameVideo.AutoSize = true;
            this.RenameVideo.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.RenameVideo.Location = new System.Drawing.Point(22, 203);
            this.RenameVideo.Name = "RenameVideo";
            this.RenameVideo.Size = new System.Drawing.Size(169, 20);
            this.RenameVideo.TabIndex = 26;
            this.RenameVideo.Text = "修改视频文件名（根据字幕）";
            this.RenameVideo.UseVisualStyleBackColor = true;
            this.RenameVideo.CheckedChanged += new System.EventHandler(this.RenameVideo_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(47, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "通常是修改字幕文件名，勾选根据字幕来修改视频文件名";
            // 
            // listBoxVideoExtension
            // 
            this.listBoxVideoExtension.FormattingEnabled = true;
            this.listBoxVideoExtension.ItemHeight = 17;
            this.listBoxVideoExtension.Location = new System.Drawing.Point(423, 61);
            this.listBoxVideoExtension.Name = "listBoxVideoExtension";
            this.listBoxVideoExtension.Size = new System.Drawing.Size(120, 208);
            this.listBoxVideoExtension.TabIndex = 28;
            // 
            // listBoxSubExtension
            // 
            this.listBoxSubExtension.FormattingEnabled = true;
            this.listBoxSubExtension.ItemHeight = 17;
            this.listBoxSubExtension.Location = new System.Drawing.Point(549, 61);
            this.listBoxSubExtension.Name = "listBoxSubExtension";
            this.listBoxSubExtension.Size = new System.Drawing.Size(120, 208);
            this.listBoxSubExtension.TabIndex = 29;
            // 
            // labelVideoExtension
            // 
            this.labelVideoExtension.AutoSize = true;
            this.labelVideoExtension.Location = new System.Drawing.Point(420, 9);
            this.labelVideoExtension.Name = "labelVideoExtension";
            this.labelVideoExtension.Size = new System.Drawing.Size(68, 17);
            this.labelVideoExtension.TabIndex = 30;
            this.labelVideoExtension.Text = "视频扩展名";
            // 
            // labelSubExtension
            // 
            this.labelSubExtension.AutoSize = true;
            this.labelSubExtension.Location = new System.Drawing.Point(547, 9);
            this.labelSubExtension.Name = "labelSubExtension";
            this.labelSubExtension.Size = new System.Drawing.Size(68, 17);
            this.labelSubExtension.TabIndex = 31;
            this.labelSubExtension.Text = "字幕扩展名";
            // 
            // btnAddVideoExtension
            // 
            this.btnAddVideoExtension.Location = new System.Drawing.Point(423, 32);
            this.btnAddVideoExtension.Name = "btnAddVideoExtension";
            this.btnAddVideoExtension.Size = new System.Drawing.Size(43, 23);
            this.btnAddVideoExtension.TabIndex = 32;
            this.btnAddVideoExtension.Text = "+";
            this.btnAddVideoExtension.UseVisualStyleBackColor = true;
            this.btnAddVideoExtension.Click += new System.EventHandler(this.AddVideoExtension);
            // 
            // btnDeleteVideoExtension
            // 
            this.btnDeleteVideoExtension.Location = new System.Drawing.Point(472, 32);
            this.btnDeleteVideoExtension.Name = "btnDeleteVideoExtension";
            this.btnDeleteVideoExtension.Size = new System.Drawing.Size(43, 23);
            this.btnDeleteVideoExtension.TabIndex = 33;
            this.btnDeleteVideoExtension.Text = "-";
            this.btnDeleteVideoExtension.UseVisualStyleBackColor = true;
            this.btnDeleteVideoExtension.Click += new System.EventHandler(this.DeleteVideoExtension);
            // 
            // btnDeleteSubExtension
            // 
            this.btnDeleteSubExtension.Location = new System.Drawing.Point(598, 32);
            this.btnDeleteSubExtension.Name = "btnDeleteSubExtension";
            this.btnDeleteSubExtension.Size = new System.Drawing.Size(43, 23);
            this.btnDeleteSubExtension.TabIndex = 35;
            this.btnDeleteSubExtension.Text = "-";
            this.btnDeleteSubExtension.UseVisualStyleBackColor = true;
            this.btnDeleteSubExtension.Click += new System.EventHandler(this.DeleteSubExtension);
            // 
            // btnAddSubExtension
            // 
            this.btnAddSubExtension.Location = new System.Drawing.Point(549, 32);
            this.btnAddSubExtension.Name = "btnAddSubExtension";
            this.btnAddSubExtension.Size = new System.Drawing.Size(43, 23);
            this.btnAddSubExtension.TabIndex = 34;
            this.btnAddSubExtension.Text = "+";
            this.btnAddSubExtension.UseVisualStyleBackColor = true;
            this.btnAddSubExtension.Click += new System.EventHandler(this.AddSubExtension);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(737, 346);
            this.Controls.Add(this.btnDeleteSubExtension);
            this.Controls.Add(this.btnAddSubExtension);
            this.Controls.Add(this.btnDeleteVideoExtension);
            this.Controls.Add(this.btnAddVideoExtension);
            this.Controls.Add(this.labelSubExtension);
            this.Controls.Add(this.labelVideoExtension);
            this.Controls.Add(this.listBoxSubExtension);
            this.Controls.Add(this.listBoxVideoExtension);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RenameVideo);
            this.Controls.Add(this.BlogLabel);
            this.Controls.Add(this.RawSubtitleBuckup);
            this.Controls.Add(this.ListItemRemovePrompt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ListShowFileFullName);
            this.Controls.Add(this.AuthorLinkLabel);
            this.Controls.Add(this.GithubLinkLabel);
            this.Controls.Add(this.UpdateLinkLabel);
            this.Controls.Add(this.VersionText);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox RawSubtitleBuckup;
        private System.Windows.Forms.Label VersionText;
        private System.Windows.Forms.LinkLabel UpdateLinkLabel;
        private System.Windows.Forms.LinkLabel GithubLinkLabel;
        private System.Windows.Forms.LinkLabel AuthorLinkLabel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox ListItemRemovePrompt;
        private System.Windows.Forms.CheckBox ListShowFileFullName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel BlogLabel;
        private System.Windows.Forms.CheckBox RenameVideo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxVideoExtension;
        private System.Windows.Forms.ListBox listBoxSubExtension;
        private System.Windows.Forms.Label labelVideoExtension;
        private System.Windows.Forms.Label labelSubExtension;
        private System.Windows.Forms.Button btnAddVideoExtension;
        private System.Windows.Forms.Button btnDeleteVideoExtension;
        private System.Windows.Forms.Button btnDeleteSubExtension;
        private System.Windows.Forms.Button btnAddSubExtension;
    }
}