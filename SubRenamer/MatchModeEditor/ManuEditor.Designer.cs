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
            this.V_Begin = new System.Windows.Forms.TextBox();
            this.Test_S = new System.Windows.Forms.TextBox();
            this.Test_V = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TestResult_S = new System.Windows.Forms.TextBox();
            this.TestResult_V = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.S_Begin = new System.Windows.Forms.TextBox();
            this.S_End = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.V_End = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(608, 564);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(141, 40);
            this.CancelBtn.TabIndex = 8;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(461, 564);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(141, 40);
            this.SaveBtn.TabIndex = 7;
            this.SaveBtn.Text = "确定";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // V_Begin
            // 
            this.V_Begin.Location = new System.Drawing.Point(17, 76);
            this.V_Begin.Multiline = true;
            this.V_Begin.Name = "V_Begin";
            this.V_Begin.Size = new System.Drawing.Size(350, 80);
            this.V_Begin.TabIndex = 1;
            // 
            // Test_S
            // 
            this.Test_S.Location = new System.Drawing.Point(135, 83);
            this.Test_S.Name = "Test_S";
            this.Test_S.Size = new System.Drawing.Size(461, 35);
            this.Test_S.TabIndex = 6;
            // 
            // Test_V
            // 
            this.Test_V.Location = new System.Drawing.Point(135, 37);
            this.Test_V.Name = "Test_V";
            this.Test_V.Size = new System.Drawing.Size(461, 35);
            this.Test_V.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TestResult_S);
            this.groupBox2.Controls.Add(this.TestResult_V);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.Test_V);
            this.groupBox2.Controls.Add(this.Test_S);
            this.groupBox2.Location = new System.Drawing.Point(12, 403);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(737, 142);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "匹配测试";
            // 
            // TestResult_S
            // 
            this.TestResult_S.Location = new System.Drawing.Point(602, 83);
            this.TestResult_S.Name = "TestResult_S";
            this.TestResult_S.ReadOnly = true;
            this.TestResult_S.Size = new System.Drawing.Size(121, 35);
            this.TestResult_S.TabIndex = 15;
            this.TestResult_S.TabStop = false;
            // 
            // TestResult_V
            // 
            this.TestResult_V.Location = new System.Drawing.Point(602, 37);
            this.TestResult_V.Name = "TestResult_V";
            this.TestResult_V.ReadOnly = true;
            this.TestResult_V.Size = new System.Drawing.Size(121, 35);
            this.TestResult_V.TabIndex = 9;
            this.TestResult_V.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 28);
            this.label6.TabIndex = 13;
            this.label6.Text = "字幕文件名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 28);
            this.label5.TabIndex = 11;
            this.label5.Text = "视频文件名";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.S_Begin);
            this.groupBox3.Controls.Add(this.S_End);
            this.groupBox3.Location = new System.Drawing.Point(12, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(737, 178);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "字幕 文件名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 28);
            this.label3.TabIndex = 10;
            this.label3.Text = "集数 [后] 的文字";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 28);
            this.label4.TabIndex = 9;
            this.label4.Text = "集数 [前] 的文字";
            // 
            // S_Begin
            // 
            this.S_Begin.Location = new System.Drawing.Point(17, 76);
            this.S_Begin.Multiline = true;
            this.S_Begin.Name = "S_Begin";
            this.S_Begin.Size = new System.Drawing.Size(350, 80);
            this.S_Begin.TabIndex = 3;
            // 
            // S_End
            // 
            this.S_End.Location = new System.Drawing.Point(373, 76);
            this.S_End.Multiline = true;
            this.S_End.Name = "S_End";
            this.S_End.Size = new System.Drawing.Size(350, 80);
            this.S_End.TabIndex = 4;
            this.S_End.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.V_Begin);
            this.groupBox1.Controls.Add(this.V_End);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(737, 178);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频 文件名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 28);
            this.label1.TabIndex = 9;
            this.label1.Text = "集数 [前] 的文字";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 28);
            this.label2.TabIndex = 10;
            this.label2.Text = "集数 [后] 的文字";
            // 
            // V_End
            // 
            this.V_End.Location = new System.Drawing.Point(373, 76);
            this.V_End.Multiline = true;
            this.V_End.Name = "V_End";
            this.V_End.Size = new System.Drawing.Size(350, 80);
            this.V_End.TabIndex = 2;
            // 
            // ManuEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 620);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TextBox V_Begin;
        private System.Windows.Forms.TextBox Test_S;
        private System.Windows.Forms.TextBox Test_V;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox S_Begin;
        private System.Windows.Forms.TextBox S_End;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TestResult_S;
        private System.Windows.Forms.TextBox TestResult_V;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox V_End;
    }
}