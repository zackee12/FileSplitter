namespace FileSplitter
{
    partial class FormMain
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
            this.groupBox_FileSplit = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_FolderBrowse = new System.Windows.Forms.Button();
            this.textBox_Path = new System.Windows.Forms.TextBox();
            this.button_FileBrowse = new System.Windows.Forms.Button();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Split = new System.Windows.Forms.Button();
            this.numericUpDown_SplitSize = new System.Windows.Forms.NumericUpDown();
            this.comboBox_ByteSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_stepSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_SplitNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStripProgressBar_Split = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox_FileSplit.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SplitSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_stepSize)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_FileSplit
            // 
            this.groupBox_FileSplit.Controls.Add(this.label2);
            this.groupBox_FileSplit.Controls.Add(this.label1);
            this.groupBox_FileSplit.Controls.Add(this.button_FolderBrowse);
            this.groupBox_FileSplit.Controls.Add(this.textBox_Path);
            this.groupBox_FileSplit.Controls.Add(this.button_FileBrowse);
            this.groupBox_FileSplit.Controls.Add(this.textBox_File);
            this.groupBox_FileSplit.Location = new System.Drawing.Point(12, 12);
            this.groupBox_FileSplit.Name = "groupBox_FileSplit";
            this.groupBox_FileSplit.Size = new System.Drawing.Size(399, 100);
            this.groupBox_FileSplit.TabIndex = 1;
            this.groupBox_FileSplit.TabStop = false;
            this.groupBox_FileSplit.Text = "Split File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File";
            // 
            // button_FolderBrowse
            // 
            this.button_FolderBrowse.Location = new System.Drawing.Point(318, 56);
            this.button_FolderBrowse.Name = "button_FolderBrowse";
            this.button_FolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.button_FolderBrowse.TabIndex = 3;
            this.button_FolderBrowse.Text = "Browse";
            this.button_FolderBrowse.UseVisualStyleBackColor = true;
            this.button_FolderBrowse.Click += new System.EventHandler(this.button_FolderBrowse_Click);
            // 
            // textBox_Path
            // 
            this.textBox_Path.Location = new System.Drawing.Point(7, 59);
            this.textBox_Path.Name = "textBox_Path";
            this.textBox_Path.Size = new System.Drawing.Size(305, 20);
            this.textBox_Path.TabIndex = 2;
            this.textBox_Path.TextChanged += new System.EventHandler(this.userInputChanged);
            // 
            // button_FileBrowse
            // 
            this.button_FileBrowse.Location = new System.Drawing.Point(318, 17);
            this.button_FileBrowse.Name = "button_FileBrowse";
            this.button_FileBrowse.Size = new System.Drawing.Size(75, 23);
            this.button_FileBrowse.TabIndex = 1;
            this.button_FileBrowse.Text = "Browse";
            this.button_FileBrowse.UseVisualStyleBackColor = true;
            this.button_FileBrowse.Click += new System.EventHandler(this.button_FileBrowse_Click);
            // 
            // textBox_File
            // 
            this.textBox_File.Location = new System.Drawing.Point(7, 20);
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(305, 20);
            this.textBox_File.TabIndex = 0;
            this.textBox_File.TextChanged += new System.EventHandler(this.userInputChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericUpDown_stepSize);
            this.groupBox2.Controls.Add(this.numericUpDown_SplitSize);
            this.groupBox2.Controls.Add(this.comboBox_ByteSize);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // button_Split
            // 
            this.button_Split.Enabled = false;
            this.button_Split.Location = new System.Drawing.Point(158, 118);
            this.button_Split.Name = "button_Split";
            this.button_Split.Size = new System.Drawing.Size(253, 100);
            this.button_Split.TabIndex = 4;
            this.button_Split.Text = "Split";
            this.button_Split.UseVisualStyleBackColor = true;
            this.button_Split.Click += new System.EventHandler(this.button_Split_Click);
            // 
            // numericUpDown_SplitSize
            // 
            this.numericUpDown_SplitSize.Location = new System.Drawing.Point(10, 18);
            this.numericUpDown_SplitSize.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_SplitSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_SplitSize.Name = "numericUpDown_SplitSize";
            this.numericUpDown_SplitSize.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown_SplitSize.TabIndex = 3;
            this.numericUpDown_SplitSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_SplitSize.ValueChanged += new System.EventHandler(this.userInputChanged);
            // 
            // comboBox_ByteSize
            // 
            this.comboBox_ByteSize.FormattingEnabled = true;
            this.comboBox_ByteSize.Items.AddRange(new object[] {
            "B",
            "kB",
            "MB"});
            this.comboBox_ByteSize.Location = new System.Drawing.Point(86, 18);
            this.comboBox_ByteSize.Name = "comboBox_ByteSize";
            this.comboBox_ByteSize.Size = new System.Drawing.Size(41, 21);
            this.comboBox_ByteSize.TabIndex = 2;
            this.comboBox_ByteSize.SelectedIndexChanged += new System.EventHandler(this.userInputChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Split File Size";
            // 
            // numericUpDown_stepSize
            // 
            this.numericUpDown_stepSize.Location = new System.Drawing.Point(10, 57);
            this.numericUpDown_stepSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_stepSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_stepSize.Name = "numericUpDown_stepSize";
            this.numericUpDown_stepSize.Size = new System.Drawing.Size(117, 20);
            this.numericUpDown_stepSize.TabIndex = 4;
            this.numericUpDown_stepSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Memory Step Size (MB)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar_Split,
            this.toolStripStatusLabel_SplitNum});
            this.statusStrip1.Location = new System.Drawing.Point(0, 229);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(420, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_SplitNum
            // 
            this.toolStripStatusLabel_SplitNum.Name = "toolStripStatusLabel_SplitNum";
            this.toolStripStatusLabel_SplitNum.Size = new System.Drawing.Size(62, 17);
            this.toolStripStatusLabel_SplitNum.Text = "0 split files";
            // 
            // splitWorker
            // 
            this.splitWorker.WorkerReportsProgress = true;
            this.splitWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.splitWorker_DoWork);
            this.splitWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.splitWorker_ProgressChanged);
            // 
            // toolStripProgressBar_Split
            // 
            this.toolStripProgressBar_Split.Name = "toolStripProgressBar_Split";
            this.toolStripProgressBar_Split.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar_Split.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 251);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_Split);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox_FileSplit);
            this.MaximumSize = new System.Drawing.Size(436, 289);
            this.MinimumSize = new System.Drawing.Size(436, 289);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox_FileSplit.ResumeLayout(false);
            this.groupBox_FileSplit.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SplitSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_stepSize)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_FileSplit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_FolderBrowse;
        private System.Windows.Forms.TextBox textBox_Path;
        private System.Windows.Forms.Button button_FileBrowse;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Split;
        private System.Windows.Forms.NumericUpDown numericUpDown_SplitSize;
        private System.Windows.Forms.ComboBox comboBox_ByteSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown_stepSize;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_SplitNum;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar_Split;
        private System.ComponentModel.BackgroundWorker splitWorker;
    }
}