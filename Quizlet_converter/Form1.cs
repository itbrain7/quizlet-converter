using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quizlet_converter
{
    public partial class Form1 : Form, Printable
    {

		bool USE_PROGRESSBAR = false;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button_openfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
			//openFileDialog.DefaultExt = "html";


			//openFileDialog.Filter="Excel files (*.xls,*xlsx)|*.xls;*xlsx|All files (*.*)|*.*";

			openFileDialog.Filter = "All files|*.*|Html Files(*.html;*.htm)|*.html;*.htm|Text Files(*.txt;*.ini)|*.txt;*.text;*.ini";

			openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length > 0)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
					printLn("Selected file : " + filename);
					this.textBox1.Text = filename;
                }
            }       
        }

		private void button_opendirectory_Click(object sender, EventArgs e)
		{

			using (var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					this.textBox1.Text = fbd.SelectedPath;

					string[] files = Directory.GetFiles(fbd.SelectedPath);

					printLn("Selected directory : " + fbd.SelectedPath);
					printLn("Files found: " + files.Length.ToString());
				}
			}
		}

		private void button_convert_Click(object sender, EventArgs e)
        {

			textBox2.Text = "";

            if (USE_PROGRESSBAR)
			{
				progressBar1.Value = 0;
			}

			string input_file = textBox1.Text;

			if (input_file.Length==0)
            {
				log("파일이나 디렉토리를 먼저 선택해주세요.", true);
				return;
            }

			if (!Directory.Exists(input_file) && !File.Exists(input_file))
            {
				log("파일 경로가 유효하지 않습니다.", true);
				return;
			}

			button_convert.Enabled = false;
			backgroundWorker1.RunWorkerAsync();
		}

		void worker_DoWork(object sender, DoWorkEventArgs e)
        {

			string input_file = textBox1.Text;

            DirectoryInfo output_dir = AppConfig.getOutputDir(input_file);


            if (Directory.Exists(input_file)) {
				int cnt = Fn.start_directory(new DirectoryInfo(input_file), output_dir, 0, this);				
			}
			else
			{
               FileInfo output_file = AppConfig.getOutputFile(input_file, output_dir);
                File.WriteAllText(output_file.FullName, "");

               Fn.start_file(new FileInfo(input_file), output_file, 0, true, this);
			}			
		}

		// Progress 리포트 - UI Thread
		void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage < 100) { 
				if (USE_PROGRESSBAR) this.progressBar1.Value = e.ProgressPercentage;
			}
		}

		// 작업 완료 - UI Thread
		void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

			// 에러가 있는지 체크
			if (e.Error != null)
			{
				errorLn(e.Error.Message);
			}
			else
            {
				string input_file = textBox1.Text;

				if (USE_PROGRESSBAR) progressBar1.Value = 100;

                printLn(AppConfig.last_output_file);

                if (Directory.Exists(input_file))
				{
					log("입력한 디렉토리안의 파일 컨버팅이 완료되었습니다.", true);
				}
				else
				{
					log("입력한 파일 컨버팅 완료되었습니다 : ", true);
				}
			}
			button_convert.Enabled = true;
		}

		private void log(String s, bool showMessage)
        {
			textBox2.AppendText(DateTime.Now.ToString("HH:mm:ss ") + s);
            textBox2.AppendText("\r\n");
			if (showMessage)
            {
				MessageBox.Show(s);
            }
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			
			backgroundWorker1.WorkerReportsProgress = true;
			backgroundWorker1.WorkerSupportsCancellation = true;
			backgroundWorker1.DoWork += new DoWorkEventHandler(worker_DoWork);
			backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
			backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

		}

        public void printLn(string msg)
        {
            textBox2.AppendText(DateTime.Now.ToString("HH:mm:ss ") + msg + "\r\n");
        }

        public void errorLn(string msg)
        {
			printLn("ERROR: " + msg);
        }

        public void reportProgress(int percent)
        {
            backgroundWorker1.ReportProgress(percent);
        }
    }
}
