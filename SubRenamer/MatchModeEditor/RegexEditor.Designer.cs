namespace SubRenamer.MatchModeEditor
{
    partial class RegexEditor
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
            this.VideoRegex = new System.Windows.Forms.TextBox();
            this.SubRegex = new System.Windows.Forms.TextBox();
            this.RegexTestLink = new System.Windows.Forms.LinkLabel();
            this.LearnRegexLink = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.V_TestStr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.V_Test_OpenBtn = new System.Windows.Forms.Button();
            this.V_TestResult = new System.Windows.Forms.TextBox();
            this.S_TestResult = new System.Windows.Forms.TextBox();
            this.S_Test_OpenBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.S_TestStr = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(564, 555);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(141, 40);
            this.CancelBtn.TabIndex = 4;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(417, 555);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(141, 40);
            this.SaveBtn.TabIndex = 3;
            this.SaveBtn.Text = "确定";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // VideoRegex
            // 
            this.VideoRegex.Location = new System.Drawing.Point(15, 43);
            this.VideoRegex.Multiline = true;
            this.VideoRegex.Name = "VideoRegex";
            this.VideoRegex.Size = new System.Drawing.Size(663, 120);
            this.VideoRegex.TabIndex = 1;
            // 
            // SubRegex
            // 
            this.SubRegex.Location = new System.Drawing.Point(15, 46);
            this.SubRegex.Multiline = true;
            this.SubRegex.Name = "SubRegex";
            this.SubRegex.Size = new System.Drawing.Size(663, 120);
            this.SubRegex.TabIndex = 2;
            // 
            // RegexTestLink
            // 
            this.RegexTestLink.AutoSize = true;
            this.RegexTestLink.Location = new System.Drawing.Point(116, 561);
            this.RegexTestLink.Name = "RegexTestLink";
            this.RegexTestLink.Size = new System.Drawing.Size(96, 28);
            this.RegexTestLink.TabIndex = 9;
            this.RegexTestLink.TabStop = true;
            this.RegexTestLink.Text = "正则测试";
            this.RegexTestLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RegexTestLink_LinkClicked);
            // 
            // LearnRegexLink
            // 
            this.LearnRegexLink.AutoSize = true;
            this.LearnRegexLink.Location = new System.Drawing.Point(14, 561);
            this.LearnRegexLink.Name = "LearnRegexLink";
            this.LearnRegexLink.Size = new System.Drawing.Size(96, 28);
            this.LearnRegexLink.TabIndex = 10;
            this.LearnRegexLink.TabStop = true;
            this.LearnRegexLink.Text = "学习正则";
            this.LearnRegexLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LearnRegexLink_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.VideoRegex);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 185);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频文件 正则表达式";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SubRegex);
            this.groupBox2.Location = new System.Drawing.Point(12, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(693, 185);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字幕文件 正则表达式";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.S_TestResult);
            this.groupBox3.Controls.Add(this.S_Test_OpenBtn);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.S_TestStr);
            this.groupBox3.Controls.Add(this.V_TestResult);
            this.groupBox3.Controls.Add(this.V_Test_OpenBtn);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.V_TestStr);
            this.groupBox3.Location = new System.Drawing.Point(12, 395);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(693, 140);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "匹配测试";
            // 
            // V_TestStr
            // 
            this.V_TestStr.Location = new System.Drawing.Point(70, 42);
            this.V_TestStr.Name = "V_TestStr";
            this.V_TestStr.Size = new System.Drawing.Size(449, 35);
            this.V_TestStr.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "视频";
            // 
            // V_Test_OpenBtn
            // 
            this.V_Test_OpenBtn.Location = new System.Drawing.Point(525, 42);
            this.V_Test_OpenBtn.Name = "V_Test_OpenBtn";
            this.V_Test_OpenBtn.Size = new System.Drawing.Size(50, 35);
            this.V_Test_OpenBtn.TabIndex = 2;
            this.V_Test_OpenBtn.Text = "...";
            this.V_Test_OpenBtn.UseVisualStyleBackColor = true;
            // 
            // V_TestResult
            // 
            this.V_TestResult.Location = new System.Drawing.Point(590, 42);
            this.V_TestResult.Name = "V_TestResult";
            this.V_TestResult.ReadOnly = true;
            this.V_TestResult.Size = new System.Drawing.Size(88, 35);
            this.V_TestResult.TabIndex = 3;
            // 
            // S_TestResult
            // 
            this.S_TestResult.Location = new System.Drawing.Point(590, 83);
            this.S_TestResult.Name = "S_TestResult";
            this.S_TestResult.ReadOnly = true;
            this.S_TestResult.Size = new System.Drawing.Size(88, 35);
            this.S_TestResult.TabIndex = 7;
            // 
            // S_Test_OpenBtn
            // 
            this.S_Test_OpenBtn.Location = new System.Drawing.Point(525, 83);
            this.S_Test_OpenBtn.Name = "S_Test_OpenBtn";
            this.S_Test_OpenBtn.Size = new System.Drawing.Size(50, 35);
            this.S_Test_OpenBtn.TabIndex = 6;
            this.S_Test_OpenBtn.Text = "...";
            this.S_Test_OpenBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "字幕";
            // 
            // S_TestStr
            // 
            this.S_TestStr.Location = new System.Drawing.Point(70, 83);
            this.S_TestStr.Name = "S_TestStr";
            this.S_TestStr.Size = new System.Drawing.Size(449, 35);
            this.S_TestStr.TabIndex = 4;
            // 
            // RegexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 608);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LearnRegexLink);
            this.Controls.Add(this.RegexTestLink);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegexEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "正则模式匹配设置";
            this.Load += new System.EventHandler(this.RegexEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TextBox VideoRegex;
        private System.Windows.Forms.TextBox SubRegex;
        private System.Windows.Forms.LinkLabel RegexTestLink;
        private System.Windows.Forms.LinkLabel LearnRegexLink;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox V_TestResult;
        private System.Windows.Forms.Button V_Test_OpenBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox V_TestStr;
        private System.Windows.Forms.TextBox S_TestResult;
        private System.Windows.Forms.Button S_Test_OpenBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox S_TestStr;
    }
}