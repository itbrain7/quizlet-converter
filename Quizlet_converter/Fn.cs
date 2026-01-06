using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Quizlet_converter
{
    internal class Fn
    {

        /// <summary>
        /// html에서 책정보를 파싱한다.
        /// </summary>
        /// <param name="html">html파일
        /// <param name="printable"></param>
        /// <returns></returns>
        public async static Task<BookInfo> parseBookInfo(String fileName, String html, Printable printable)
        {

            // Create an AngleSharp configuration 
            var config = AngleSharp.Configuration.Default.WithDefaultLoader();
            // Create browser session using config
            var context = AngleSharp.BrowsingContext.New(config);
            // Load HTML into Anglesharp
            var document = await context.OpenAsync(req => req.Content(html));

            // Query for page title

            if (document == null)
            {
                printable.errorLn("document is null");
                return null;
            }

            string bookName = document.QuerySelector("div.s1jt6060")?.TextContent ?? null;

            if (string.IsNullOrEmpty(bookName))
            {
                string titleText = document.QuerySelector("title")?.TextContent;

                if (!string.IsNullOrEmpty(titleText))
                {
                    // HTML 엔티티 디코딩 (&#x27; → ')
                    titleText = WebUtility.HtmlDecode(titleText).Trim();

                    string[] suffixes =
                    {
                       "'s Library Vocab Words 낱말 카드 | Quizlet"
                    };

                    foreach (var suffix in suffixes)
                    {
                        if (titleText.EndsWith(suffix))
                        {
                            bookName = titleText.Substring(0, titleText.Length - suffix.Length).Trim();
                            break;
                        }
                    }
                }
            }
            /// h1-> class: tall2k

            printable.printLn($"bookName=>{bookName}");

            string writer = document.QuerySelector("span.UserLink-username--typography-subheading-3")?.TextContent ?? null;

            printable.printLn($"writer=>{writer}");

            //var sectorList = document.All.Where(e => e.LocalName == "td" && e.ClassList.Contains("col_type1")).ToList();

            List<Word> word_list = new List<Word>();

            if (AppConfig.METHOD_VERSION == 1)
            {
                var word_tag_list = document.QuerySelectorAll("span.TermText").ToList();

                printable.printLn("word_list.count=>" + word_tag_list.Count);

                // 홀수번째가 단어이고, 짝수번째가 그 단어에 대한 설명임           
                for (int i = 0; i < word_tag_list.Count; i = i + 2)
                {

                    IElement word_tag = word_tag_list[i];

                    String eng = word_tag.Text();
                    String definition = word_tag_list[i + 1].Text();

                    Word word = new Word(eng, definition);

                    printable.printLn($"eng=>{word.getEng()}, definition=>{word.getDefinition()}");

                    if (word != null)
                    {
                        word_list.Add(word);
                    }
                }
            }
            else
            {
                List<string> eng_list = FnCommon.getInsideList(html,
                    "\\\"word\\\",\\\"media\\\":[{\\\"type\\\":1,\\\"plainText\\\":\\\"", "\\\"");

                List<string> definition_list = FnCommon.getInsideList(html,
                    "\\\"definition\\\",\\\"media\\\":[{\\\"type\\\":1,\\\"plainText\\\":\\\"", "\\\"");

                printable.printLn("word_list.count=>" + (eng_list != null ? eng_list.Count : "null"));

                if (eng_list != null && eng_list.Count > 0)
                {
                    for (int i = 0; i < eng_list.Count; ++i)
                    {
                        String eng = eng_list[i];
                        String definition = i < definition_list.Count ? definition_list[i] : null;
                        Word word = new Word(eng, definition);
                        word_list.Add(word);
                    }
                }
            }

            return new BookInfo(fileName, bookName, writer, word_list);
        }



        public static int start_directory(DirectoryInfo input_dir, DirectoryInfo output_dir, int directory_idx, Printable printable)
        {

            IEnumerable<FileInfo> file_list = Directory.GetFiles(input_dir.FullName).Select(f => new FileInfo(f)).OrderBy(f => f.CreationTime); ;
            int seq = 0;
            int all_seq = 0;

            if (file_list != null && file_list.Count() > 0)
            {
                FileInfo output_file = AppConfig.getOutputFile((directory_idx == 0 ? input_dir.Name : "[" + directory_idx + "]" + input_dir.Name), output_dir);

                File.AppendAllText(output_file.FullName, BookInfo.ToLinesHeader());

                foreach (FileInfo input_file in file_list)
                {
                    seq++;

                    int word_cnt=start_file(input_file, output_file, all_seq, false, printable);
                    all_seq = all_seq + word_cnt;
                    printable.reportProgress(seq * 100 / file_list.Count());
                }

                AppConfig.last_output_file = output_file.FullName;

                if (directory_idx == 0)
                {
                    FnCommon.OpenNotePad(AppConfig.last_output_file);
                }
            }

            ///////////////////
            // 서비디렉토리도 퍄싱한다.

            IEnumerable<DirectoryInfo> sub_directory_list = Directory.GetDirectories(input_dir.FullName).Select(f => new DirectoryInfo(f)).OrderBy(f => f.CreationTime); ;
            int subdir_idx = directory_idx + 1;

            foreach (DirectoryInfo sub_dir in sub_directory_list)
            {
                seq++;
                int sub_directory_cnt = start_directory(sub_dir, output_dir, subdir_idx, printable);

                subdir_idx = subdir_idx + sub_directory_cnt + 1;
                //backgroundWorker1.ReportProgress(seq * 100 / file_list.Count());
            }

            if (directory_idx == 0 && sub_directory_list != null && sub_directory_list.Count() > 0)
            {
                if (directory_idx == 0)
                {
                    FnCommon.OpenExplorer(output_dir.FullName);
                }
            }


            return sub_directory_list.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input_file"></param>
        /// <param name="output_file"></param>
        /// <param name="all_seq"></param>
        /// <param name="only_one"></param>
        /// <param name="printable"></param>
        /// <returns>wordcnt를 리턴한다. </returns>
        public static int start_file(FileInfo input_file, FileInfo output_file, int all_seq, bool only_one, Printable printable)
        {

            if (!input_file.Exists)
            {
                return 0;
            }

            if (only_one) printable.reportProgress(10);

            string input_file_string = System.IO.File.ReadAllText(input_file.FullName);

            Task<BookInfo> task = Fn.parseBookInfo(input_file.Name, input_file_string, printable);

            if (only_one)
            {
                printable.reportProgress(90);
                File.AppendAllText(output_file.FullName, BookInfo.ToLinesHeader());
            }

            File.AppendAllText(output_file.FullName, task.Result.ToLines(all_seq));

            if (only_one)
            {
                printable.reportProgress(100);
                AppConfig.last_output_file = output_file.FullName;

                FnCommon.OpenNotePad(AppConfig.last_output_file);
            }
            return task.Result.getWordcnt();
        }
    }
}