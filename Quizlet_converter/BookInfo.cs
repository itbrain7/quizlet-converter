using AngleSharp.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace Quizlet_converter
{
    /// <summary>
    /// 책의 정보 (제목,작성자,단어목록)
    /// </summary>
    internal class BookInfo
    {

        String fileName;

        /// <summary>
        /// 제목
        /// </summary>
        String title;

        /// <summary>
        /// 작성자
        /// </summary>
        String writer;

        /// <summary>
        /// 단어목록
        /// </summary>
         List<String> words;

        public String getTitle() { return title; }   

        public String getWriter() { return writer; }

        public int getWordcnt() { 
            return words!=null ? words.Count : 0; 
        }

        public List<string> getWords() { return words; }    

        public BookInfo(String fileName, String title, String writer, List<String> words)
        {
            this.fileName = fileName;
            this.title = title;
            this.writer = writer;
            this.words = words;
        }

        public override String ToString()
        {
            return $"fileName={fileName}, title ={title}, writer={writer}, words.cnt={getWordcnt()}";
        }

        public static String ToLinesHeader()
        {
            return "fileName\ttitle\twriter\tno\tword\r\n";
        }

        public String ToLines()
        {
           StringBuilder sb=new StringBuilder();

            for (int i=0;i<words.Count;++i)
            {

                sb.Append(fileName).Append("\t").Append(title).Append("\t").Append(writer).Append("\t").Append(i+1).Append("\t");

                sb.Append(words[i]).Append("\r\n");             
             
            }

            return sb.ToString();
        }
    }
}
