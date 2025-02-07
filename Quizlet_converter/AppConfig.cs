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

        public static String last_output_file;

        /// <summary>
        /// 입력파일 or 디렉토리를 기준으로 해서 최종 생성되는 파일경로를 리턴한다.
        /// </summary>
        /// <param name="input_file_or_directory"></param>
        /// <returns></returns>
        public static FileInfo getOutputFile(String input_file_or_directory)
        {

            String output_subdir = @"Quizlet_converted_" + DateTime.Now.ToString("yyyyMMdd_HHmm");

             String output_dir=
                USE_TEMPORARY_DIRECTORY?
                Path.Combine(System.IO.Path.GetTempPath(), output_subdir)
                :
                Path.Combine(System.IO.Directory.GetParent(input_file_or_directory).FullName, output_subdir);

            if (!Directory.Exists(output_dir))
            {
                Directory.CreateDirectory(output_dir);
            }

            if (Directory.Exists(input_file_or_directory))
            {

                String output_name = "output.txt";
                return new FileInfo(Path.Combine(output_dir, "output_" + Path.GetFileName(input_file_or_directory)) + ".txt");
            }
            else if (File.Exists(input_file_or_directory))
            {
                return new FileInfo(Path.Combine(output_dir, "output_" + Path.GetFileName(input_file_or_directory))+ ".txt");
            }
            else
            {
                return null;
            }

        }
    }
}
