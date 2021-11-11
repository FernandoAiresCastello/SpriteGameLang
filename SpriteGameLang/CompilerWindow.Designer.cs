namespace SpriteGameLang
{
    partial class CompilerWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtFile = new System.Windows.Forms.TextBox();
            this.BtnCompile = new System.Windows.Forms.Button();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "SGL Program File:";
            // 
            // TxtFile
            // 
            this.TxtFile.Location = new System.Drawing.Point(133, 14);
            this.TxtFile.Name = "TxtFile";
            this.TxtFile.Size = new System.Drawing.Size(394, 22);
            this.TxtFile.TabIndex = 1;
            // 
            // BtnCompile
            // 
            this.BtnCompile.Image = global::SpriteGameLang.Properties.Resources.compile;
            this.BtnCompile.Location = new System.Drawing.Point(593, 12);
            this.BtnCompile.Name = "BtnCompile";
            this.BtnCompile.Size = new System.Drawing.Size(55, 26);
            this.BtnCompile.TabIndex = 3;
            this.BtnCompile.UseVisualStyleBackColor = true;
            this.BtnCompile.Click += new System.EventHandler(this.BtnCompile_Click);
            // 
            // BtnSelect
            // 
            this.BtnSelect.Image = global::SpriteGameLang.Properties.Resources.folder;
            this.BtnSelect.Location = new System.Drawing.Point(536, 12);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(55, 26);
            this.BtnSelect.TabIndex = 2;
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtLog);
            this.groupBox1.Location = new System.Drawing.Point(16, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(632, 359);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // TxtLog
            // 
            this.TxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLog.Location = new System.Drawing.Point(10, 25);
            this.TxtLog.Multiline = true;
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ReadOnly = true;
            this.TxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtLog.Size = new System.Drawing.Size(612, 324);
            this.TxtLog.TabIndex = 0;
            this.TxtLog.WordWrap = false;
            // 
            // CompilerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 415);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnCompile);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.TxtFile);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CompilerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpriteGameLang - Compiler Frontend";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtFile;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.Button BtnCompile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtLog;
    }
}