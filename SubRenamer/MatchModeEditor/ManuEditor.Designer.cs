namespace SubRenamer.MatchModeEditor
{
    partial class ManuEditor
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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.V_Tpl = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.S_Matched = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.S_OpenBtn = new System.Windows.Forms.Button();
            this.S_Tpl = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.V_Matched = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.V_OpenBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(792, 553);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(141, 40);
            this.CancelBtn.TabIndex = 8;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(646, 553);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(141, 40);
            this.SaveBtn.TabIndex = 7;
            this.SaveBtn.Text = "确定";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // V_Tpl
            // 
            this.V_Tpl.Location = new System.Drawing.Point(17, 43);
            this.V_Tpl.Multiline = true;
            this.V_Tpl.Name = "V_Tpl";
            this.V_Tpl.Size = new System.Drawing.Size(703, 178);
            this.V_Tpl.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.S_Matched);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.S_OpenBtn);
            this.groupBox3.Controls.Add(this.S_Tpl);
            this.groupBox3.Location = new System.Drawing.Point(12, 261);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(921, 243);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "字幕 文件名";
            // 
            // S_Matched
            // 
            this.S_Matched.Location = new System.Drawing.Point(727, 186);
            this.S_Matched.Name = "S_Matched";
            this.S_Matched.ReadOnly = true;
            this.S_Matched.Size = new System.Drawing.Size(180, 35);
            this.S_Matched.TabIndex = 12;
            this.S_Matched.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(726, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 75);
            this.label2.TabIndex = 4;
            this.label2.Text = "任意选择一个文件\r\n然后，手动将左侧\r\n集数文字替换成 <X>";
            // 
            // S_OpenBtn
            // 
            this.S_OpenBtn.Location = new System.Drawing.Point(727, 43);
            this.S_OpenBtn.Name = "S_OpenBtn";
            this.S_OpenBtn.Size = new System.Drawing.Size(180, 47);
            this.S_OpenBtn.TabIndex = 3;
            this.S_OpenBtn.Text = "选择字幕文件";
            this.S_OpenBtn.UseVisualStyleBackColor = true;
            // 
            // S_Tpl
            // 
            this.S_Tpl.Location = new System.Drawing.Point(17, 43);
            this.S_Tpl.Multiline = true;
            this.S_Tpl.Name = "S_Tpl";
            this.S_Tpl.Size = new System.Drawing.Size(703, 178);
            this.S_Tpl.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.V_Matched);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.V_OpenBtn);
            this.groupBox1.Controls.Add(this.V_Tpl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(921, 243);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频 文件名";
            // 
            // V_Matched
            // 
            this.V_Matched.Location = new System.Drawing.Point(727, 186);
            this.V_Matched.Name = "V_Matched";
            this.V_Matched.ReadOnly = true;
            this.V_Matched.Size = new System.Drawing.Size(180, 35);
            this.V_Matched.TabIndex = 13;
            this.V_Matched.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(726, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 75);
            this.label1.TabIndex = 3;
            this.label1.Text = "任意选择一个文件\r\n然后，手动将左侧\r\n集数文字替换成 <X>\r\n";
            // 
            // V_OpenBtn
            // 
            this.V_OpenBtn.Location = new System.Drawing.Point(727, 43);
            this.V_OpenBtn.Name = "V_OpenBtn";
            this.V_OpenBtn.Size = new System.Drawing.Size(180, 47);
            this.V_OpenBtn.TabIndex = 2;
            this.V_OpenBtn.Text = "选择视频文件";
            this.V_OpenBtn.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 7.714286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(12, 518);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(585, 75);
            this.label3.TabIndex = 5;
            this.label3.Text = "(支持通配符 *，表示任意字符) 举个栗子：\r\n   文件名 \"[Steins;Gate][11][BDrip][1080P] 时空边界的教理.sc.ass\"\r\n" +
    "      改为 \"[Steins;Gate][<X>][BDrip]*.sc.ass\"";
            // 
            // ManuEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 611);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManuEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手动模式匹配设置";
            this.Load += new System.EventHandler(this.ManuEditor_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TextBox V_Tpl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox S_Tpl;
        private System.Windows.Forms.Button V_OpenBtn;
        private System.Windows.Forms.Button S_OpenBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox V_Matched;
        private System.Windows.Forms.TextBox S_Matched;
    }
}