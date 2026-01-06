
namespace Quizlet_converter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button_openfile = new System.Windows.Forms.Button();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            textBox1 = new System.Windows.Forms.TextBox();
            button_convert = new System.Windows.Forms.Button();
            textBox2 = new System.Windows.Forms.TextBox();
            button_opendirectory = new System.Windows.Forms.Button();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // button_openfile
            // 
            button_openfile.Location = new System.Drawing.Point(15, 13);
            button_openfile.Margin = new System.Windows.Forms.Padding(4);
            button_openfile.Name = "button_openfile";
            button_openfile.Size = new System.Drawing.Size(285, 55);
            button_openfile.TabIndex = 0;
            button_openfile.Text = "1. 파일 선택";
            button_openfile.UseVisualStyleBackColor = true;
            button_openfile.Click += button_openfile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(15, 103);
            textBox1.Margin = new System.Windows.Forms.Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(976, 27);
            textBox1.TabIndex = 1;
            // 
            // button_convert
            // 
            button_convert.Location = new System.Drawing.Point(15, 148);
            button_convert.Margin = new System.Windows.Forms.Padding(4);
            button_convert.Name = "button_convert";
            button_convert.Size = new System.Drawing.Size(285, 52);
            button_convert.TabIndex = 2;
            button_convert.Text = "3. 컨버팅하기";
            button_convert.UseVisualStyleBackColor = true;
            button_convert.Click += button_convert_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            textBox2.Location = new System.Drawing.Point(15, 222);
            textBox2.Margin = new System.Windows.Forms.Padding(4);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBox2.Size = new System.Drawing.Size(976, 360);
            textBox2.TabIndex = 3;
            // 
            // button_opendirectory
            // 
            button_opendirectory.Location = new System.Drawing.Point(360, 12);
            button_opendirectory.Margin = new System.Windows.Forms.Padding(4);
            button_opendirectory.Name = "button_opendirectory";
            button_opendirectory.Size = new System.Drawing.Size(235, 55);
            button_opendirectory.TabIndex = 4;
            button_opendirectory.Text = "2. 디렉토리 선택";
            button_opendirectory.UseVisualStyleBackColor = true;
            button_opendirectory.Click += button_opendirectory_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(320, 148);
            progressBar1.Margin = new System.Windows.Forms.Padding(4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(681, 52);
            progressBar1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(320, 29);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(23, 20);
            label1.TabIndex = 6;
            label1.Text = "or";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(380, 73);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(199, 20);
            label2.TabIndex = 7;
            label2.Text = "(서브디렉토리까지도 가능）";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1008, 615);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(progressBar1);
            Controls.Add(button_opendirectory);
            Controls.Add(textBox2);
            Controls.Add(button_convert);
            Controls.Add(textBox1);
            Controls.Add(button_openfile);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4);
            Name = "Form1";
            Text = "Quizlet Converter 3.3";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button_openfile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_convert;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_opendirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

