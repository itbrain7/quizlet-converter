using AngleSharp.Dom;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

            string bookName = document.QuerySelector("div.s1jt6060") ?.TextContent ?? null;

            printable.printLn($"bookName=>{bookName}");

            string writer = document.QuerySelector("span.UserLink-username--typography-subheading-3")?.TextContent ?? null;

            printable.printLn($"writer=>{writer}");

            //var sectorList = document.All.Where(e => e.LocalName == "td" && e.ClassList.Contains("col_type1")).ToList();

           var word_tag_list = document.QuerySelectorAll("span.TermText").ToList();

            printable.printLn("word_list.count=>" + word_tag_list.Count);

            List<String> word_list = new List<string>();

            // 홀수번째가 단어이고, 짝수번째가 그 단어에 대한 설명임
            for (int i=0; i<word_tag_list.Count; i=i+2) 
            {

                IElement word_tag = word_tag_list[i];

                String word = word_tag.Text();

                word= word.Replace("\t", " ");
                word = word.Replace("\r\n", " ");
                word = word.Replace("\r", " ");
                word = word.Replace("\n", " ");

                word = word.Replace("\t", " ");

                word = word.Replace("  ", " ");

                printable.printLn($"word=>{word}");

                if (word != null) {
                    word_list.Add(word.Trim());
                }
            }

            return new BookInfo(fileName, bookName, writer, word_list);
        }

    }
}
