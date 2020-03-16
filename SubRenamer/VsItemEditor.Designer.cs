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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.MatchKey_TextBox = new System.Windows.Forms.TextBox();
            this.Video_TextBox = new System.Windows.Forms.TextBox();
            this.Sub_TextBox = new System.Windows.Forms.TextBox();
            this.Video_SelectFileBtn = new System.Windows.Forms.Button();
            this.Sub_SelectFileBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PrevItemBtn = new System.Windows.Forms.Button();
            this.NextItemBtn = new System.Windows.Forms.Button();
            this.Video_ClearBtn = new System.Windows.Forms.Button();
            this.Sub_ClearBtn = new System.Windows.Forms.Button();
            this.RemoveItemBtn = new System.Windows.Forms.Button();
            this.AddItemBtn = new System.Windows.Forms.Button();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PageNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
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
            this.MatchKey_TextBox.Size = new System.Drawing.Size(494, 35);
            this.MatchKey_TextBox.TabIndex = 1;
            this.MatchKey_TextBox.TextChanged += new System.EventHandler(this.MatchKey_TextBox_TextChanged);
            // 
            // Video_TextBox
            // 
            this.Video_TextBox.Location = new System.Drawing.Point(76, 60);
            this.Video_TextBox.Name = "Video_TextBox";
            this.Video_TextBox.ReadOnly = true;
            this.Video_TextBox.Size = new System.Drawing.Size(734, 35);
            this.Video_TextBox.TabIndex = 2;
            // 
            // Sub_TextBox
            // 
            this.Sub_TextBox.Location = new System.Drawing.Point(76, 101);
            this.Sub_TextBox.Name = "Sub_TextBox";
            this.Sub_TextBox.ReadOnly = true;
            this.Sub_TextBox.Size = new System.Drawing.Size(734, 35);
            this.Sub_TextBox.TabIndex = 3;
            // 
            // Video_SelectFileBtn
            // 
            this.Video_SelectFileBtn.Location = new System.Drawing.Point(817, 60);
            this.Video_SelectFileBtn.Name = "Video_SelectFileBtn";
            this.Video_SelectFileBtn.Size = new System.Drawing.Size(74, 35);
            this.Video_SelectFileBtn.TabIndex = 4;
            this.Video_SelectFileBtn.Text = "...";
            this.Video_SelectFileBtn.UseVisualStyleBackColor = true;
            this.Video_SelectFileBtn.Click += new System.EventHandler(this.Video_SelectFileBtn_Click);
            // 
            // Sub_SelectFileBtn
            // 
            this.Sub_SelectFileBtn.Location = new System.Drawing.Point(816, 104);
            this.Sub_SelectFileBtn.Name = "Sub_SelectFileBtn";
            this.Sub_SelectFileBtn.Size = new System.Drawing.Size(75, 35);
            this.Sub_SelectFileBtn.TabIndex = 5;
            this.Sub_SelectFileBtn.Text = "...";
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
            // PrevItemBtn
            // 
            this.PrevItemBtn.Location = new System.Drawing.Point(656, 19);
            this.PrevItemBtn.Name = "PrevItemBtn";
            this.PrevItemBtn.Size = new System.Drawing.Size(74, 35);
            this.PrevItemBtn.TabIndex = 10;
            this.PrevItemBtn.Text = "«";
            this.PrevItemBtn.UseVisualStyleBackColor = true;
            this.PrevItemBtn.Click += new System.EventHandler(this.PrevItemBtn_Click);
            // 
            // NextItemBtn
            // 
            this.NextItemBtn.Location = new System.Drawing.Point(817, 19);
            this.NextItemBtn.Name = "NextItemBtn";
            this.NextItemBtn.Size = new System.Drawing.Size(74, 35);
            this.NextItemBtn.TabIndex = 11;
            this.NextItemBtn.Text = "»";
            this.NextItemBtn.UseVisualStyleBackColor = true;
            this.NextItemBtn.Click += new System.EventHandler(this.NextItemBtn_Click);
            // 
            // Video_ClearBtn
            // 
            this.Video_ClearBtn.Location = new System.Drawing.Point(897, 60);
            this.Video_ClearBtn.Name = "Video_ClearBtn";
            this.Video_ClearBtn.Size = new System.Drawing.Size(74, 35);
            this.Video_ClearBtn.TabIndex = 12;
            this.Video_ClearBtn.Text = "×";
            this.Video_ClearBtn.UseVisualStyleBackColor = true;
            this.Video_ClearBtn.Click += new System.EventHandler(this.Video_ClearBtn_Click);
            // 
            // Sub_ClearBtn
            // 
            this.Sub_ClearBtn.Location = new System.Drawing.Point(897, 104);
            this.Sub_ClearBtn.Name = "Sub_ClearBtn";
            this.Sub_ClearBtn.Size = new System.Drawing.Size(74, 35);
            this.Sub_ClearBtn.TabIndex = 13;
            this.Sub_ClearBtn.Text = "×";
            this.Sub_ClearBtn.UseVisualStyleBackColor = true;
            this.Sub_ClearBtn.Click += new System.EventHandler(this.Sub_ClearBtn_Click);
            // 
            // RemoveItemBtn
            // 
            this.RemoveItemBtn.Location = new System.Drawing.Point(576, 19);
            this.RemoveItemBtn.Name = "RemoveItemBtn";
            this.RemoveItemBtn.Size = new System.Drawing.Size(74, 35);
            this.RemoveItemBtn.TabIndex = 14;
            this.RemoveItemBtn.Text = "删";
            this.RemoveItemBtn.UseVisualStyleBackColor = true;
            this.RemoveItemBtn.Click += new System.EventHandler(this.RemoveItemBtn_Click);
            // 
            // AddItemBtn
            // 
            this.AddItemBtn.Location = new System.Drawing.Point(897, 19);
            this.AddItemBtn.Name = "AddItemBtn";
            this.AddItemBtn.Size = new System.Drawing.Size(74, 35);
            this.AddItemBtn.TabIndex = 15;
            this.AddItemBtn.Text = "增";
            this.AddItemBtn.UseVisualStyleBackColor = true;
            this.AddItemBtn.Click += new System.EventHandler(this.AddItemBtn_Click);
            // 
            // PageNum
            // 
            this.PageNum.Location = new System.Drawing.Point(736, 19);
            this.PageNum.Name = "PageNum";
            this.PageNum.ReadOnly = true;
            this.PageNum.Size = new System.Drawing.Size(74, 35);
            this.PageNum.TabIndex = 16;
            this.PageNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VsItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1000, 171);
            this.Controls.Add(this.PageNum);
            this.Controls.Add(this.AddItemBtn);
            this.Controls.Add(this.RemoveItemBtn);
            this.Controls.Add(this.Sub_ClearBtn);
            this.Controls.Add(this.Video_ClearBtn);
            this.Controls.Add(this.NextItemBtn);
            this.Controls.Add(this.PrevItemBtn);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VsItemEditor_FormClosed);
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
        private System.Windows.Forms.Button PrevItemBtn;
        private System.Windows.Forms.Button NextItemBtn;
        private System.Windows.Forms.Button Video_ClearBtn;
        private System.Windows.Forms.Button Sub_ClearBtn;
        private System.Windows.Forms.Button RemoveItemBtn;
        private System.Windows.Forms.Button AddItemBtn;
        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.TextBox PageNum;
    }
}