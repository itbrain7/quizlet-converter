using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.Design.AxImporter;

namespace Quizlet_converter
{
    /// <summary>
    /// 모든 프로젝트에서 공통으로 사용할수 있는 함수 모음
    /// </summary>
    internal class FnCommon
    {


        public static void OpenNotePad(String file)
        {
            if (System.IO.File.Exists(file))
            {
                Process.Start("notepad.exe", file);
            }
            else if (Directory.Exists(file))
            {
                MessageBox.Show(file, "디렉토리명이라서 메모장으로 열수 없음");
            }
            else
            {
                MessageBox.Show(file, "존재하지 않는 파일은 메모장으로 열수 없음");
            }
        }

        /// <summary>
        /// 탐색기로 해당폴더를 연다.
        /// </summary>
        /// <param name="file"></param>
        public static void OpenExplorer(String file)
        {
            if (System.IO.File.Exists(file))
            {
                Process.Start("explorer.exe", file);
            }
            else if (Directory.Exists(file))
            {
                Process.Start("explorer.exe", file);
            }
            else
            {
                MessageBox.Show(file, "존재하지 않는 파일은 탐색기로 열수 없음");
            }
        }

        public static String HttpGet(String url, Printable printable)
        {

            String request_encoding = "UTF-8";

            string responseText = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초 
            request.ContentType = "text/html; charset=" + request_encoding;

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {

                Encoding encoding;
                if (resp.CharacterSet != null && resp.CharacterSet.ToLower() == "utf-8")
                {
                    encoding = Encoding.UTF8;
                }
                else {
                    encoding = Encoding.Default;
                }

                if (resp.CharacterSet != null)
                {
                    printable.printLn("response.CharacterSet=>" + resp.CharacterSet);
                }

                HttpStatusCode status = resp.StatusCode;
                Console.WriteLine(status);  // 정상이면 "OK"

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream, encoding))
                {
                    responseText = sr.ReadToEnd();
                }

                if (resp.CharacterSet.Equals("EUC-KR"))
                {
                    printable.printLn("EUCKR_TO_UTF8");
                    printable.printLn(EUCKR_TO_UTF8(responseText));
                }
                else
                {
                    printable.printLn("NO ENCODING CHANGE");
                    printable.printLn(responseText);
                }

                Console.WriteLine(responseText);



            }

            return responseText;
        }

        /// <summary>
        /// 가장 최근파일을 가져온다.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static String getLatestFile(String folder, String searchPattern)
        {
            var directory = new DirectoryInfo(folder);

            FileInfo myFile = directory.GetFiles(searchPattern)
                           .OrderByDescending(f => f.LastWriteTime)
                           .First();

            return myFile.FullName;
        }

        public static String getInside(String src, String start, String end) {
            return getInside(src, start, end, true);
        }

        /// <summary>
        /// Start와 end사이의 문자열을 찾는다.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="start">시작문자</param>
        /// <param name="end">끝문자</param>
        /// <param name="end_is_mandatory">끝문자가 반드시 있어야 하는지</param>
        /// <returns></returns>
        public static String getInside(String src, String start, String end, bool end_is_mandatory)
        {
            if (src == null) return null;

            int pos_start = src.IndexOf(start);
            if (pos_start < 0) return null;

            int pos_end = src.IndexOf(end, pos_start + start.Length);
            if (pos_end < 0)
            {
                if (end_is_mandatory)
                {
                    return null;
                }
                else
                {
                    return src.Substring(pos_start + start.Length, src.Length - pos_start - start.Length);
                }
            }
            return src.Substring(pos_start + start.Length, pos_end - pos_start - start.Length);
        }


        /// <summary>
        /// Start와 end사이의 문자열List를 찾는다.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="start">시작문자</param>
        /// <param name="end">끝문자</param>
       /// <returns></returns>
        public static List<String> getInsideList(String src, String start, String end)
        {
            if (src == null) return null;

            List<String> list = new List<String>();

            int startIndex = 0;

            while (true)
            {
                int pos_start = src.IndexOf(start, startIndex);
                if (pos_start < 0) break;

                int pos_end = src.IndexOf(end, pos_start + start.Length);
                if (pos_end < 0) break;

                list.Add(src.Substring(pos_start + start.Length, pos_end - pos_start - start.Length));

                startIndex = pos_end + end.Length;
            }

            return list;
        }

        /// <summary>
        /// JSON value값을 찾는다. (예, "archiveCode":"wr_waout_f11")
        /// </summary>
        /// <param name="src">찾으려는 대상  문자열</param>
        /// <param name="var">찾으려는 변수명</param>
        /// <returns></returns>
        public static String getJsonValue(String src, String var)
        {
            if (src == null) return null;

            String var_ext = "\"" + var + "\"";

            int pos_var = src.IndexOf(var_ext);
            if (pos_var < 0) return null;

            int pos_value_start = src.IndexOf("\"", pos_var + var_ext.Length);
            if (pos_value_start < 0) return null;

            int pos_value_end = src.IndexOf("\"", pos_value_start + 1);
            if (pos_value_end < 0) return null;

            return src.Substring(pos_value_start + 1, pos_value_end - pos_value_start-1);

        }

        public static string UTF8_TO_EUCKR(string strUTF8)
        {

            // ''EUC-KR' is not a supported encoding name` 2025-01-19

            return Encoding.GetEncoding("EUC-KR").GetString(
                Encoding.Convert(
                Encoding.UTF8,
                Encoding.GetEncoding("EUC-KR"),
                Encoding.UTF8.GetBytes(strUTF8)));
        }

        //euc-kr 문자열을 UTF8문자열로 변환한다.
        public static string EUCKR_TO_UTF8(string strEUCKR)
        {
            // Prevent the Exception ('EUC-KR' is not a supported encoding name) 2025-01-19
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            byte[] bb = Encoding.GetEncoding("EUC-KR").GetBytes(strEUCKR);

            return Encoding.GetEncoding("UTF-8").GetString(bb);

                /*
            return Encoding.UTF8.GetString(
                   Encoding.Convert(
                   Encoding.GetEncoding("EUC-KR"),
                   Encoding.UTF8,
                   Encoding.GetEncoding("EUC-KR").GetBytes(strEUCKR)));
                */
        }

        public static bool Equals(String str1, String str2)
        {
            return str1 == null && str2 == null || str1.Equals(str2);
        }

        /// <summary>
        /// 괄호왼쪽 문자열을 리턴한다.
        /// 예) 2차전지(23) ==> 2차전지
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String LeftFromBrackets(String str)
        {
            int idxLeft = str.IndexOf('(');
            if (idxLeft < 0) return null;

            int idxRight= str.IndexOf(")", idxLeft+1);
            if (idxRight < 0) return null;

            return str.Substring(0, idxLeft);
        }

        /// <summary>
        /// URL에서 마지막 슬래시 다음의 파일명을 리턴한다.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static String Url2FileName(String url)
        {
            if (url == null) return null;
            if (url.Length == 0) return "";

            int pos_slash = url.LastIndexOf("/");

            int start = pos_slash + 1;

            return url.Substring(start, url.Length - start);           
        }


        /// <summary>
        /// URL에서 확장자 앞쪽의 파일명Base을 리턴한다.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static String Url2FileBase(String url)
        {
            if (url == null) return null;
            if (url.Length == 0) return "";

            int pos_slash = url.LastIndexOf("/");

            int start = pos_slash + 1;
          
            int pos_period = url.LastIndexOf(".");
            if (pos_period >= 0)
            {
                return url.Substring(start, pos_period- start);
            }
            else
            {
                return url.Substring(start, url.Length - start);
            };
        }


    }



}
