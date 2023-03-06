using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quizlet_converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_openfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.DefaultExt = "html";
            //openFileDialog.Filter = "Html Files(*.html)|Text Files(*.txt;*.ini)";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length > 0)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
					log("Selected file : " + filename);
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

					log("Selected directory : " + fbd.SelectedPath);
					log("Files found: " + files.Length.ToString());
				}
			}
		}

		private void button_convert_Click(object sender, EventArgs e)
        {
			string input_file = textBox1.Text;

			if (input_file.Length==0)
            {
				log("파일이나 디렉토리를 먼저 선택해주세요.", true);
				return;
            }

			if (Directory.Exists(input_file))
            {
				button_convert.Enabled = false;
				int cnt=start_directory(input_file);
				button_convert.Enabled = true;
				log("디렉토리안의 파일 : " + cnt + "개가 컨버팅 완료되었습니다.", true);
			}
			else if (File.Exists(input_file))
            {
				button_convert.Enabled = false;
				string output_file=start_file(input_file);
				button_convert.Enabled = true;
				log("컨버팅 완료되었습니다 : " + output_file, true);
			}
			else
            {
				log("파일 경로가 유효하지 않습니다.", true);
				return;
            }
        }

        private void log(String s, bool showMessage)
        {
            textBox2.AppendText(s);
            textBox2.AppendText("\r\n");
			if (showMessage)
            {
				MessageBox.Show(s);
            }
        }

		private void log(String s)
        {
			log(s, false);
        }

		private void log(int i)
        {
			log(i.ToString(), false);
        }


		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private string replace_first(string src, string old_value, string new_value)
        {
			int found = src.IndexOf(old_value);
			if (found >= 0)
			{
				string temp = src.Substring(0, src.IndexOf(old_value));
				string temp2 = src.Substring(src.IndexOf(old_value) + old_value.Length);
				return temp + new_value + temp2;
			}
			return src;
		}

		/// <summary>
		/// 문자열을 시작문자열과 끝문자열을 기준으로 잘라서, 크기가 3인 문자열배열을 리턴한다.
		/// (시작문자열 못찾으면 크기가 1인 문자열배열, 시작문자열은 찾고, 끝문자열을 못찾으면 크기가 2인 문자열)
		/// </summary>
		/// <param name="src">원문자열</param>
		/// <param name="string_start">찾는시작 문자열</param>
		/// <param name="string_end">찾기끝 문자열 </param>
		/// <returns></returns>
		private string[] splits(string src, string string_start, string string_end)
        {

			if (src == null) return null;

			int found_start, found_end, cut_end;
			
			found_start = src.IndexOf(string_start);
			if (found_start < 0) return new string[] { src };

			string str_first = src.Substring(0, found_start);
			found_end = src.IndexOf(string_end, found_start + string_start.Length);

			if (found_end < 0) return new string[] { str_first, src.Substring(found_start + string_start.Length) };
			cut_end = found_end + string_end.Length;

			return new string[] { str_first, src.Substring(found_start + string_start.Length, found_end-found_start- (string_start.Length) ), src.Substring(cut_end) };		
		}

		private int start_directory(string input_dir)
        {
			if (!Directory.Exists(input_dir))
            {
				log("Directory is not existed (" + input_dir + ")", true);
				return 0;
			}

			int cnt = 0;

			foreach (string input_file in Directory.GetFiles(input_dir)) 
            {
				start_file(input_file);
				cnt++;
            }

			return cnt;
        }

		private string start_file(string input_file) 
		{		
		
			if (Directory.Exists(input_file)) { 
				log("input_dir is not a file(" + input_file + ")");
				return null;
			}

			string str = System.IO.File.ReadAllText(input_file);

			string output_dir = System.IO.Directory.GetParent(input_file) + @"\Quizlet_converted";
			if (!Directory.Exists(output_dir))
            {
				Directory.CreateDirectory(output_dir);
            }
			
			string output_file = output_dir + @"\" + Path.GetFileName(input_file);
			
			string new_str = str;
			string LINE = "<!------------------------------------>";
		
			while (true) {

				//log("new_str.Length=" + new_str.Length);
				string[] ss = splits(new_str, "<div style=\"display:none\">", "</div>");
				if (ss.Length>=3) {				
					String new_s1 = ss[1];
				
					if (new_s1.IndexOf("</span><span>")>=0) {				
					
						while (true) {
							String new_s1_changed = replace_first(new_s1, "</span><span>", "\t");
							new_s1_changed=replace_first(new_s1_changed, "</span><span>", "\r\n");
							if (new_s1==new_s1_changed) {
								break;
							}
							new_s1 = new_s1_changed;
						}
					
						new_s1 = new_s1.Replace("<span>", "\r\n");
						new_s1 = new_s1.Replace("</span>", "\r\n");
						new_str = ss[0] + "\r\n" + LINE + "\r\n" + "<div style=\"display:none\" leedo>" + new_s1 + "</div>\r\n" + LINE + "\r\n" + ss[2];
					}
					else
					{
						new_str = ss[0] + new_s1 + ss[2];
					}
				}
				else
				{
					break;
				}
			}

			File.WriteAllText(output_file, new_str);
			return output_file;
		}

    }
}
