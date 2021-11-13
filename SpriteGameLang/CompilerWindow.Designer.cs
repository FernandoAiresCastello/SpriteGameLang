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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtLog = new System.Windows.Forms.TextBox();
            this.BtnAbout = new System.Windows.Forms.Button();
            this.BtnViewGenerated = new System.Windows.Forms.Button();
            this.BtnOpenInExplorer = new System.Windows.Forms.Button();
            this.BtnRun = new System.Windows.Forms.Button();
            this.BtnEditProgram = new System.Windows.Forms.Button();
            this.BtnCompile = new System.Windows.Forms.Button();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source code file:";
            // 
            // TxtFile
            // 
            this.TxtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtFile.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFile.Location = new System.Drawing.Point(16, 36);
            this.TxtFile.Name = "TxtFile";
            this.TxtFile.Size = new System.Drawing.Size(797, 20);
            this.TxtFile.TabIndex = 0;
            this.TxtFile.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtLog);
            this.groupBox1.Location = new System.Drawing.Point(171, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(642, 406);
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
            this.TxtLog.Size = new System.Drawing.Size(622, 371);
            this.TxtLog.TabIndex = 0;
            this.TxtLog.TabStop = false;
            this.TxtLog.WordWrap = false;
            // 
            // BtnAbout
            // 
            this.BtnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAbout.Image = global::SpriteGameLang.Properties.Resources.information;
            this.BtnAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAbout.Location = new System.Drawing.Point(16, 437);
            this.BtnAbout.Name = "BtnAbout";
            this.BtnAbout.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BtnAbout.Size = new System.Drawing.Size(143, 32);
            this.BtnAbout.TabIndex = 7;
            this.BtnAbout.Text = "  About";
            this.BtnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnAbout.UseVisualStyleBackColor = true;
            this.BtnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // BtnViewGenerated
            // 
            this.BtnViewGenerated.Image = global::SpriteGameLang.Properties.Resources.page_white_cplusplus;
            this.BtnViewGenerated.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnViewGenerated.Location = new System.Drawing.Point(16, 240);
            this.BtnViewGenerated.Name = "BtnViewGenerated";
            this.BtnViewGenerated.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BtnViewGenerated.Size = new System.Drawing.Size(143, 32);
            this.BtnViewGenerated.TabIndex = 6;
            this.BtnViewGenerated.Text = "  Open C++";
            this.BtnViewGenerated.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnViewGenerated.UseVisualStyleBackColor = true;
            this.BtnViewGenerated.Click += new System.EventHandler(this.BtnViewGenerated_Click);
            // 
            // BtnOpenInExplorer
            // 
            this.BtnOpenInExplorer.Image = global::SpriteGameLang.Properties.Resources.folder;
            this.BtnOpenInExplorer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnOpenInExplorer.Location = new System.Drawing.Point(16, 104);
            this.BtnOpenInExplorer.Name = "BtnOpenInExplorer";
            this.BtnOpenInExplorer.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BtnOpenInExplorer.Size = new System.Drawing.Size(143, 32);
            this.BtnOpenInExplorer.TabIndex = 2;
            this.BtnOpenInExplorer.Text = "  Open folder";
            this.BtnOpenInExplorer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnOpenInExplorer.UseVisualStyleBackColor = true;
            this.BtnOpenInExplorer.Click += new System.EventHandler(this.BtnOpenInExplorer_Click);
            // 
            // BtnRun
            // 
            this.BtnRun.Image = global::SpriteGameLang.Properties.Resources.application_go;
            this.BtnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRun.Location = new System.Drawing.Point(16, 206);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BtnRun.Size = new System.Drawing.Size(143, 32);
            this.BtnRun.TabIndex = 5;
            this.BtnRun.Text = "  Run";
            this.BtnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // BtnEditProgram
            // 
            this.BtnEditProgram.Image = global::SpriteGameLang.Properties.Resources.page_white_edit;
            this.BtnEditProgram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEditProgram.Location = new System.Drawing.Point(16, 138);
            this.BtnEditProgram.Name = "BtnEditProgram";
            this.BtnEditProgram.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BtnEditProgram.Size = new System.Drawing.Size(143, 32);
            this.BtnEditProgram.TabIndex = 3;
            this.BtnEditProgram.Text = "  Open file";
            this.BtnEditProgram.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnEditProgram.UseVisualStyleBackColor = true;
            this.BtnEditProgram.Click += new System.EventHandler(this.BtnEditProgram_Click);
            // 
            // BtnCompile
            // 
            this.BtnCompile.Image = global::SpriteGameLang.Properties.Resources.compile;
            this.BtnCompile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCompile.Location = new System.Drawing.Point(16, 172);
            this.BtnCompile.Name = "BtnCompile";
            this.BtnCompile.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BtnCompile.Size = new System.Drawing.Size(143, 32);
            this.BtnCompile.TabIndex = 4;
            this.BtnCompile.Text = "  Compile & Run";
            this.BtnCompile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCompile.UseMnemonic = false;
            this.BtnCompile.UseVisualStyleBackColor = true;
            this.BtnCompile.Click += new System.EventHandler(this.BtnCompile_Click);
            // 
            // BtnSelect
            // 
            this.BtnSelect.Image = global::SpriteGameLang.Properties.Resources.folder_explorer;
            this.BtnSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSelect.Location = new System.Drawing.Point(16, 70);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BtnSelect.Size = new System.Drawing.Size(143, 32);
            this.BtnSelect.TabIndex = 1;
            this.BtnSelect.Text = "  Choose file";
            this.BtnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // CompilerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 481);
            this.Controls.Add(this.BtnAbout);
            this.Controls.Add(this.BtnViewGenerated);
            this.Controls.Add(this.BtnOpenInExplorer);
            this.Controls.Add(this.BtnRun);
            this.Controls.Add(this.BtnEditProgram);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnCompile);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.TxtFile);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CompilerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpriteGameLang Compiler";
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
        private System.Windows.Forms.Button BtnEditProgram;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.Button BtnOpenInExplorer;
        private System.Windows.Forms.Button BtnViewGenerated;
        private System.Windows.Forms.Button BtnAbout;
    }
}