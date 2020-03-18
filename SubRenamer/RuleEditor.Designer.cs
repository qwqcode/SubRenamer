namespace SubRenamer
{
    partial class RuleEditor
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
            this.ModeBtn_Auto = new System.Windows.Forms.RadioButton();
            this.ModeBtn_Manu = new System.Windows.Forms.RadioButton();
            this.ModeBtn_Regex = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EditBtn_Regex = new System.Windows.Forms.LinkLabel();
            this.EditBtn_Manu = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModeBtn_Auto
            // 
            this.ModeBtn_Auto.AutoSize = true;
            this.ModeBtn_Auto.Location = new System.Drawing.Point(26, 46);
            this.ModeBtn_Auto.Name = "ModeBtn_Auto";
            this.ModeBtn_Auto.Size = new System.Drawing.Size(121, 32);
            this.ModeBtn_Auto.TabIndex = 3;
            this.ModeBtn_Auto.TabStop = true;
            this.ModeBtn_Auto.Text = "自动匹配";
            this.ModeBtn_Auto.UseVisualStyleBackColor = true;
            this.ModeBtn_Auto.CheckedChanged += new System.EventHandler(this.ModeBtn_Auto_CheckedChanged);
            // 
            // ModeBtn_Manu
            // 
            this.ModeBtn_Manu.AutoSize = true;
            this.ModeBtn_Manu.Location = new System.Drawing.Point(26, 149);
            this.ModeBtn_Manu.Name = "ModeBtn_Manu";
            this.ModeBtn_Manu.Size = new System.Drawing.Size(121, 32);
            this.ModeBtn_Manu.TabIndex = 4;
            this.ModeBtn_Manu.TabStop = true;
            this.ModeBtn_Manu.Text = "手动匹配";
            this.ModeBtn_Manu.UseVisualStyleBackColor = true;
            this.ModeBtn_Manu.CheckedChanged += new System.EventHandler(this.ModeBtn_Manu_CheckedChanged);
            // 
            // ModeBtn_Regex
            // 
            this.ModeBtn_Regex.AutoSize = true;
            this.ModeBtn_Regex.Location = new System.Drawing.Point(26, 260);
            this.ModeBtn_Regex.Name = "ModeBtn_Regex";
            this.ModeBtn_Regex.Size = new System.Drawing.Size(121, 32);
            this.ModeBtn_Regex.TabIndex = 5;
            this.ModeBtn_Regex.TabStop = true;
            this.ModeBtn_Regex.Text = "正则匹配";
            this.ModeBtn_Regex.UseVisualStyleBackColor = true;
            this.ModeBtn_Regex.CheckedChanged += new System.EventHandler(this.ModeBtn_Regex_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.EditBtn_Regex);
            this.groupBox1.Controls.Add(this.EditBtn_Manu);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ModeBtn_Auto);
            this.groupBox1.Controls.Add(this.ModeBtn_Regex);
            this.groupBox1.Controls.Add(this.ModeBtn_Manu);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 351);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "匹配模式";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label4.Location = new System.Drawing.Point(363, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 28);
            this.label4.TabIndex = 11;
            this.label4.Text = "qwqaq.com";
            // 
            // EditBtn_Regex
            // 
            this.EditBtn_Regex.AutoSize = true;
            this.EditBtn_Regex.Location = new System.Drawing.Point(153, 262);
            this.EditBtn_Regex.Name = "EditBtn_Regex";
            this.EditBtn_Regex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EditBtn_Regex.Size = new System.Drawing.Size(54, 28);
            this.EditBtn_Regex.TabIndex = 10;
            this.EditBtn_Regex.TabStop = true;
            this.EditBtn_Regex.Text = "编辑";
            this.EditBtn_Regex.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EditBtn_Regex_LinkClicked);
            // 
            // EditBtn_Manu
            // 
            this.EditBtn_Manu.AutoSize = true;
            this.EditBtn_Manu.Location = new System.Drawing.Point(153, 149);
            this.EditBtn_Manu.Name = "EditBtn_Manu";
            this.EditBtn_Manu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EditBtn_Manu.Size = new System.Drawing.Size(54, 28);
            this.EditBtn_Manu.TabIndex = 9;
            this.EditBtn_Manu.TabStop = true;
            this.EditBtn_Manu.Text = "编辑";
            this.EditBtn_Manu.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EditBtn_Manu_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(26, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(348, 28);
            this.label3.TabIndex = 8;
            this.label3.Text = "手动输入用于集数匹配的正则表达式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(26, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(327, 28);
            this.label2.TabIndex = 7;
            this.label2.Text = "手动输入文件名中集数前后的文字";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(26, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 28);
            this.label1.TabIndex = 6;
            this.label1.Text = "通过比较两个文件名的差异来匹配";
            // 
            // RuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(524, 378);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RuleEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "规则编辑";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RuleEditor_FormClosed);
            this.Load += new System.EventHandler(this.RuleEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton ModeBtn_Manu;
        private System.Windows.Forms.RadioButton ModeBtn_Regex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel EditBtn_Regex;
        private System.Windows.Forms.LinkLabel EditBtn_Manu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton ModeBtn_Auto;
    }
}