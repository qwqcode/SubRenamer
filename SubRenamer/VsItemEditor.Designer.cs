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
            this.PrevItemBtn = new System.Windows.Forms.Button();
            this.NextItemBtn = new System.Windows.Forms.Button();
            this.Video_ClearBtn = new System.Windows.Forms.Button();
            this.Sub_ClearBtn = new System.Windows.Forms.Button();
            this.RemoveItemBtn = new System.Windows.Forms.Button();
            this.AddItemBtn = new System.Windows.Forms.Button();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PageNum = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "匹配";
            // 
            // MatchKey_TextBox
            // 
            this.MatchKey_TextBox.Location = new System.Drawing.Point(78, 22);
            this.MatchKey_TextBox.Name = "MatchKey_TextBox";
            this.MatchKey_TextBox.Size = new System.Drawing.Size(322, 35);
            this.MatchKey_TextBox.TabIndex = 1;
            this.MatchKey_TextBox.TextChanged += new System.EventHandler(this.MatchKey_TextBox_TextChanged);
            // 
            // Video_TextBox
            // 
            this.Video_TextBox.Location = new System.Drawing.Point(20, 34);
            this.Video_TextBox.Multiline = true;
            this.Video_TextBox.Name = "Video_TextBox";
            this.Video_TextBox.ReadOnly = true;
            this.Video_TextBox.Size = new System.Drawing.Size(734, 114);
            this.Video_TextBox.TabIndex = 2;
            // 
            // Sub_TextBox
            // 
            this.Sub_TextBox.Location = new System.Drawing.Point(20, 34);
            this.Sub_TextBox.Multiline = true;
            this.Sub_TextBox.Name = "Sub_TextBox";
            this.Sub_TextBox.ReadOnly = true;
            this.Sub_TextBox.Size = new System.Drawing.Size(734, 114);
            this.Sub_TextBox.TabIndex = 5;
            // 
            // Video_SelectFileBtn
            // 
            this.Video_SelectFileBtn.Location = new System.Drawing.Point(760, 34);
            this.Video_SelectFileBtn.Name = "Video_SelectFileBtn";
            this.Video_SelectFileBtn.Size = new System.Drawing.Size(74, 35);
            this.Video_SelectFileBtn.TabIndex = 3;
            this.Video_SelectFileBtn.Text = "...";
            this.Video_SelectFileBtn.UseVisualStyleBackColor = true;
            this.Video_SelectFileBtn.Click += new System.EventHandler(this.Video_SelectFileBtn_Click);
            // 
            // Sub_SelectFileBtn
            // 
            this.Sub_SelectFileBtn.Location = new System.Drawing.Point(760, 34);
            this.Sub_SelectFileBtn.Name = "Sub_SelectFileBtn";
            this.Sub_SelectFileBtn.Size = new System.Drawing.Size(75, 35);
            this.Sub_SelectFileBtn.TabIndex = 6;
            this.Sub_SelectFileBtn.Text = "...";
            this.Sub_SelectFileBtn.UseVisualStyleBackColor = true;
            this.Sub_SelectFileBtn.Click += new System.EventHandler(this.Sub_SelectFileBtn_Click);
            // 
            // PrevItemBtn
            // 
            this.PrevItemBtn.Location = new System.Drawing.Point(513, 22);
            this.PrevItemBtn.Name = "PrevItemBtn";
            this.PrevItemBtn.Size = new System.Drawing.Size(74, 35);
            this.PrevItemBtn.TabIndex = 8;
            this.PrevItemBtn.Text = "«";
            this.PrevItemBtn.UseVisualStyleBackColor = true;
            this.PrevItemBtn.Click += new System.EventHandler(this.PrevItemBtn_Click);
            // 
            // NextItemBtn
            // 
            this.NextItemBtn.Location = new System.Drawing.Point(673, 22);
            this.NextItemBtn.Name = "NextItemBtn";
            this.NextItemBtn.Size = new System.Drawing.Size(74, 35);
            this.NextItemBtn.TabIndex = 9;
            this.NextItemBtn.Text = "»";
            this.NextItemBtn.UseVisualStyleBackColor = true;
            this.NextItemBtn.Click += new System.EventHandler(this.NextItemBtn_Click);
            // 
            // Video_ClearBtn
            // 
            this.Video_ClearBtn.Location = new System.Drawing.Point(760, 75);
            this.Video_ClearBtn.Name = "Video_ClearBtn";
            this.Video_ClearBtn.Size = new System.Drawing.Size(74, 35);
            this.Video_ClearBtn.TabIndex = 4;
            this.Video_ClearBtn.Text = "×";
            this.Video_ClearBtn.UseVisualStyleBackColor = true;
            this.Video_ClearBtn.Click += new System.EventHandler(this.Video_ClearBtn_Click);
            // 
            // Sub_ClearBtn
            // 
            this.Sub_ClearBtn.Location = new System.Drawing.Point(760, 75);
            this.Sub_ClearBtn.Name = "Sub_ClearBtn";
            this.Sub_ClearBtn.Size = new System.Drawing.Size(74, 35);
            this.Sub_ClearBtn.TabIndex = 7;
            this.Sub_ClearBtn.Text = "×";
            this.Sub_ClearBtn.UseVisualStyleBackColor = true;
            this.Sub_ClearBtn.Click += new System.EventHandler(this.Sub_ClearBtn_Click);
            // 
            // RemoveItemBtn
            // 
            this.RemoveItemBtn.Location = new System.Drawing.Point(433, 22);
            this.RemoveItemBtn.Name = "RemoveItemBtn";
            this.RemoveItemBtn.Size = new System.Drawing.Size(74, 35);
            this.RemoveItemBtn.TabIndex = 11;
            this.RemoveItemBtn.Text = "-";
            this.RemoveItemBtn.UseVisualStyleBackColor = true;
            this.RemoveItemBtn.Click += new System.EventHandler(this.RemoveItemBtn_Click);
            // 
            // AddItemBtn
            // 
            this.AddItemBtn.Location = new System.Drawing.Point(753, 22);
            this.AddItemBtn.Name = "AddItemBtn";
            this.AddItemBtn.Size = new System.Drawing.Size(74, 35);
            this.AddItemBtn.TabIndex = 10;
            this.AddItemBtn.Text = "+";
            this.AddItemBtn.UseVisualStyleBackColor = true;
            this.AddItemBtn.Click += new System.EventHandler(this.AddItemBtn_Click);
            // 
            // PageNum
            // 
            this.PageNum.Location = new System.Drawing.Point(593, 22);
            this.PageNum.Name = "PageNum";
            this.PageNum.ReadOnly = true;
            this.PageNum.Size = new System.Drawing.Size(74, 35);
            this.PageNum.TabIndex = 16;
            this.PageNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Video_TextBox);
            this.groupBox1.Controls.Add(this.Video_SelectFileBtn);
            this.groupBox1.Controls.Add(this.Video_ClearBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(845, 165);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频文件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Sub_TextBox);
            this.groupBox2.Controls.Add(this.Sub_SelectFileBtn);
            this.groupBox2.Controls.Add(this.Sub_ClearBtn);
            this.groupBox2.Location = new System.Drawing.Point(12, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(845, 165);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字幕文件";
            // 
            // VsItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(873, 424);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PageNum);
            this.Controls.Add(this.AddItemBtn);
            this.Controls.Add(this.RemoveItemBtn);
            this.Controls.Add(this.NextItemBtn);
            this.Controls.Add(this.PrevItemBtn);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button PrevItemBtn;
        private System.Windows.Forms.Button NextItemBtn;
        private System.Windows.Forms.Button Video_ClearBtn;
        private System.Windows.Forms.Button Sub_ClearBtn;
        private System.Windows.Forms.Button RemoveItemBtn;
        private System.Windows.Forms.Button AddItemBtn;
        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.TextBox PageNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}