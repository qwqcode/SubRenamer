namespace SubRenamer
{
    partial class VsItemEditor
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
            System.Windows.Forms.Button SaveBtn;
            this.label1 = new System.Windows.Forms.Label();
            this.MatchKey_TextBox = new System.Windows.Forms.TextBox();
            this.Video_TextBox = new System.Windows.Forms.TextBox();
            this.Sub_TextBox = new System.Windows.Forms.TextBox();
            this.Video_SelectFileBtn = new System.Windows.Forms.Button();
            this.Sub_SelectFileBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            SaveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new System.Drawing.Point(272, 155);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new System.Drawing.Size(155, 43);
            SaveBtn.TabIndex = 8;
            SaveBtn.Text = "确定";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "匹配";
            // 
            // MatchKey_TextBox
            // 
            this.MatchKey_TextBox.Location = new System.Drawing.Point(76, 19);
            this.MatchKey_TextBox.Name = "MatchKey_TextBox";
            this.MatchKey_TextBox.Size = new System.Drawing.Size(512, 35);
            this.MatchKey_TextBox.TabIndex = 1;
            // 
            // Video_TextBox
            // 
            this.Video_TextBox.Location = new System.Drawing.Point(76, 60);
            this.Video_TextBox.Name = "Video_TextBox";
            this.Video_TextBox.ReadOnly = true;
            this.Video_TextBox.Size = new System.Drawing.Size(351, 35);
            this.Video_TextBox.TabIndex = 2;
            // 
            // Sub_TextBox
            // 
            this.Sub_TextBox.Location = new System.Drawing.Point(76, 101);
            this.Sub_TextBox.Name = "Sub_TextBox";
            this.Sub_TextBox.ReadOnly = true;
            this.Sub_TextBox.Size = new System.Drawing.Size(351, 35);
            this.Sub_TextBox.TabIndex = 3;
            // 
            // Video_SelectFileBtn
            // 
            this.Video_SelectFileBtn.Location = new System.Drawing.Point(433, 60);
            this.Video_SelectFileBtn.Name = "Video_SelectFileBtn";
            this.Video_SelectFileBtn.Size = new System.Drawing.Size(155, 35);
            this.Video_SelectFileBtn.TabIndex = 4;
            this.Video_SelectFileBtn.Text = "选择文件";
            this.Video_SelectFileBtn.UseVisualStyleBackColor = true;
            this.Video_SelectFileBtn.Click += new System.EventHandler(this.Video_SelectFileBtn_Click);
            // 
            // Sub_SelectFileBtn
            // 
            this.Sub_SelectFileBtn.Location = new System.Drawing.Point(433, 101);
            this.Sub_SelectFileBtn.Name = "Sub_SelectFileBtn";
            this.Sub_SelectFileBtn.Size = new System.Drawing.Size(155, 35);
            this.Sub_SelectFileBtn.TabIndex = 5;
            this.Sub_SelectFileBtn.Text = "选择文件";
            this.Sub_SelectFileBtn.UseVisualStyleBackColor = true;
            this.Sub_SelectFileBtn.Click += new System.EventHandler(this.Sub_SelectFileBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 28);
            this.label2.TabIndex = 6;
            this.label2.Text = "视频";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 28);
            this.label3.TabIndex = 7;
            this.label3.Text = "字幕";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(433, 155);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(155, 43);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // VsItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(609, 220);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(SaveBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Sub_SelectFileBtn);
            this.Controls.Add(this.Video_SelectFileBtn);
            this.Controls.Add(this.Sub_TextBox);
            this.Controls.Add(this.Video_TextBox);
            this.Controls.Add(this.MatchKey_TextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VsItemEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑";
            this.Load += new System.EventHandler(this.VsItemEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MatchKey_TextBox;
        private System.Windows.Forms.TextBox Video_TextBox;
        private System.Windows.Forms.TextBox Sub_TextBox;
        private System.Windows.Forms.Button Video_SelectFileBtn;
        private System.Windows.Forms.Button Sub_SelectFileBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CancelBtn;
    }
}