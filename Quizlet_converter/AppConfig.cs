using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet_converter
{
    internal class AppConfig
    {

        /// <summary>
        /// 산출물 폴더를 만들때 시스템의 임시디텍토리를 사용하는지
        /// </summary>
        public static bool USE_TEMPORARY_DIRECTORY = true;

        /// <summary>
        /// html을 파싱할 방법 (1:html태그 2:자바스크립트)
        /// (html태그로 했더니 최대단어수가 15개로만 됨. 나머지는 자바스크립트를 이용해서 서버에서 가져오는듯함. 
        /// 그래서 자바스크립트 파싱으로 새로운 버전 생성함)
        /// </summary>
        public static int METHOD_VERSION = 2;


        public static String last_output_file;


        /// <summary>
        /// 입력파일 or 디렉토리를 기준으로 해서 최종 생성되는 디렉토리를 찾는다.
        /// </summary>
        /// <param name="input_file_or_directory"></param>
        /// <returns></returns>
        public static DirectoryInfo getOutputDir(String input_file_or_directory)
        {
            String output_subdir= @"Quizlet_converted_" + DateTime.Now.ToString("yyyyMMdd_HHmm");

            String output_dir =
              USE_TEMPORARY_DIRECTORY ?
              Path.Combine(System.IO.Path.GetTempPath(), "Quizlet", output_subdir)
              :
              Path.Combine(System.IO.Directory.GetParent(input_file_or_directory).FullName, output_subdir);

            if (!Directory.Exists(output_dir))
            {
                Directory.CreateDirectory(output_dir);
            }

            return new DirectoryInfo(output_dir);
        }

        /// <summary>
        /// 입력파일 or 디렉토리를 기준으로 해서 최종 생성되는 파일경로를 리턴한다.
        /// </summary>
        /// <param name="input_file_or_directory"></param>
        /// <returns></returns>
        public static FileInfo getOutputFile(String file_name, DirectoryInfo output_dir)
        {
            if (Directory.Exists(file_name))
            {
                return new FileInfo(Path.Combine(output_dir.FullName, Path.GetFileName(file_name)) + ".txt");
            }
            else if (File.Exists(file_name))
            {
                return new FileInfo(Path.Combine(output_dir.FullName, Path.GetFileName(file_name))+ ".txt");
            }
            else
            {
                return new FileInfo(Path.Combine(output_dir.FullName, file_name + ".txt"));
            }

        }
    }
}
