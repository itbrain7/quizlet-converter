
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
            this.button_openfile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_convert = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_opendirectory = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // button_openfile
            // 
            this.button_openfile.Location = new System.Drawing.Point(12, 9);
            this.button_openfile.Name = "button_openfile";
            this.button_openfile.Size = new System.Drawing.Size(222, 41);
            this.button_openfile.TabIndex = 0;
            this.button_openfile.Text = "파일 선택";
            this.button_openfile.UseVisualStyleBackColor = true;
            this.button_openfile.Click += new System.EventHandler(this.button_openfile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(760, 23);
            this.textBox1.TabIndex = 1;
            // 
            // button_convert
            // 
            this.button_convert.Location = new System.Drawing.Point(14, 250);
            this.button_convert.Name = "button_convert";
            this.button_convert.Size = new System.Drawing.Size(222, 39);
            this.button_convert.TabIndex = 2;
            this.button_convert.Text = "컨버팅하기";
            this.button_convert.UseVisualStyleBackColor = true;
            this.button_convert.Click += new System.EventHandler(this.button_convert_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 295);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(760, 143);
            this.textBox2.TabIndex = 3;
            // 
            // button_opendirectory
            // 
            this.button_opendirectory.Location = new System.Drawing.Point(264, 9);
            this.button_opendirectory.Name = "button_opendirectory";
            this.button_opendirectory.Size = new System.Drawing.Size(222, 41);
            this.button_opendirectory.TabIndex = 4;
            this.button_opendirectory.Text = "디렉토리 선택";
            this.button_opendirectory.UseVisualStyleBackColor = true;
            this.button_opendirectory.Click += new System.EventHandler(this.button_opendirectory_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(242, 250);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(530, 39);
            this.progressBar1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_opendirectory);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button_convert);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_openfile);
            this.Name = "Form1";
            this.Text = "Quizlet Converter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

