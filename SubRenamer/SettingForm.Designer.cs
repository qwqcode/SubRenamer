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
            this.ListShowFileFullNameCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.SettingTabs = new System.Windows.Forms.TabControl();
            this.Basic = new System.Windows.Forms.TabPage();
            this.List = new System.Windows.Forms.TabPage();
            this.SettingTabs.SuspendLayout();
            this.Basic.SuspendLayout();
            this.List.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionText
            // 
            this.VersionText.AutoSize = true;
            this.VersionText.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.VersionText.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VersionText.Location = new System.Drawing.Point(640, 468);
            this.VersionText.Name = "VersionText";
            this.VersionText.Size = new System.Drawing.Size(81, 25);
            this.VersionText.TabIndex = 16;
            this.VersionText.Text = "v0.0.0.0";
            // 
            // UpdateLinkLabel
            // 
            this.UpdateLinkLabel.AutoSize = true;
            this.UpdateLinkLabel.Location = new System.Drawing.Point(581, 467);
            this.UpdateLinkLabel.Name = "UpdateLinkLabel";
            this.UpdateLinkLabel.Size = new System.Drawing.Size(54, 28);
            this.UpdateLinkLabel.TabIndex = 17;
            this.UpdateLinkLabel.TabStop = true;
            this.UpdateLinkLabel.Text = "更新";
            this.UpdateLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.UpdateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateLinkLabel_LinkClicked);
            // 
            // GithubLinkLabel
            // 
            this.GithubLinkLabel.AutoSize = true;
            this.GithubLinkLabel.Location = new System.Drawing.Point(143, 467);
            this.GithubLinkLabel.Name = "GithubLinkLabel";
            this.GithubLinkLabel.Size = new System.Drawing.Size(96, 28);
            this.GithubLinkLabel.TabIndex = 18;
            this.GithubLinkLabel.TabStop = true;
            this.GithubLinkLabel.Text = "开源仓库";
            this.GithubLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.GithubLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLinkLabel_LinkClicked);
            // 
            // AuthorLinkLabel
            // 
            this.AuthorLinkLabel.AutoSize = true;
            this.AuthorLinkLabel.Location = new System.Drawing.Point(83, 467);
            this.AuthorLinkLabel.Name = "AuthorLinkLabel";
            this.AuthorLinkLabel.Size = new System.Drawing.Size(54, 28);
            this.AuthorLinkLabel.TabIndex = 19;
            this.AuthorLinkLabel.TabStop = true;
            this.AuthorLinkLabel.Text = "作者";
            this.AuthorLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.AuthorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AuthorLinkLabel_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(23, 467);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(54, 28);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "反馈";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // ListShowFileFullNameCheckBox
            // 
            this.ListShowFileFullNameCheckBox.AutoSize = true;
            this.ListShowFileFullNameCheckBox.Checked = global::SubRenamer.MainSettings.Default.ListShowFileFullName;
            this.ListShowFileFullNameCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SubRenamer.MainSettings.Default, "ListShowFileFullName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ListShowFileFullNameCheckBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.ListShowFileFullNameCheckBox.Location = new System.Drawing.Point(22, 55);
            this.ListShowFileFullNameCheckBox.Name = "ListShowFileFullNameCheckBox";
            this.ListShowFileFullNameCheckBox.Size = new System.Drawing.Size(190, 29);
            this.ListShowFileFullNameCheckBox.TabIndex = 16;
            this.ListShowFileFullNameCheckBox.Text = "显示文件完整路径";
            this.ListShowFileFullNameCheckBox.UseVisualStyleBackColor = true;
            this.ListShowFileFullNameCheckBox.CheckedChanged += new System.EventHandler(this.ListShowFileFullNameCheckBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = global::SubRenamer.MainSettings.Default.ListItemRemovePrompt;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SubRenamer.MainSettings.Default, "ListItemRemovePrompt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.checkBox2.Location = new System.Drawing.Point(22, 20);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(133, 29);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "删除前确认";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::SubRenamer.MainSettings.Default.OpenFolderFinished;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SubRenamer.MainSettings.Default, "OpenFolderFinished", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.checkBox1.Location = new System.Drawing.Point(22, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(190, 29);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "改名后打开文件夹";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = global::SubRenamer.MainSettings.Default.RawSubtitleBuckup;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SubRenamer.MainSettings.Default, "RawSubtitleBuckup", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.checkBox3.Location = new System.Drawing.Point(22, 55);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(209, 29);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.Text = "同目录备份原始字幕";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // SettingTabs
            // 
            this.SettingTabs.Controls.Add(this.Basic);
            this.SettingTabs.Controls.Add(this.List);
            this.SettingTabs.Location = new System.Drawing.Point(12, 12);
            this.SettingTabs.Name = "SettingTabs";
            this.SettingTabs.SelectedIndex = 0;
            this.SettingTabs.Size = new System.Drawing.Size(714, 443);
            this.SettingTabs.TabIndex = 22;
            // 
            // Basic
            // 
            this.Basic.Controls.Add(this.checkBox3);
            this.Basic.Controls.Add(this.checkBox1);
            this.Basic.Location = new System.Drawing.Point(4, 37);
            this.Basic.Name = "Basic";
            this.Basic.Padding = new System.Windows.Forms.Padding(3);
            this.Basic.Size = new System.Drawing.Size(706, 402);
            this.Basic.TabIndex = 0;
            this.Basic.Text = "基本";
            this.Basic.UseVisualStyleBackColor = true;
            // 
            // List
            // 
            this.List.Controls.Add(this.ListShowFileFullNameCheckBox);
            this.List.Controls.Add(this.checkBox2);
            this.List.Location = new System.Drawing.Point(4, 37);
            this.List.Name = "List";
            this.List.Padding = new System.Windows.Forms.Padding(3);
            this.List.Size = new System.Drawing.Size(706, 402);
            this.List.TabIndex = 1;
            this.List.Text = "列表";
            this.List.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(741, 510);
            this.Controls.Add(this.SettingTabs);
            this.Controls.Add(this.linkLabel1);
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
            this.SettingTabs.ResumeLayout(false);
            this.Basic.ResumeLayout(false);
            this.Basic.PerformLayout();
            this.List.ResumeLayout(false);
            this.List.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label VersionText;
        private System.Windows.Forms.LinkLabel UpdateLinkLabel;
        private System.Windows.Forms.LinkLabel GithubLinkLabel;
        private System.Windows.Forms.LinkLabel AuthorLinkLabel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox ListShowFileFullNameCheckBox;
        private System.Windows.Forms.TabControl SettingTabs;
        private System.Windows.Forms.TabPage Basic;
        private System.Windows.Forms.TabPage List;
    }
}