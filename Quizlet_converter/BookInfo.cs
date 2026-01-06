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
         List<Word> words;

        public String getTitle() { return title; }   

        public String getWriter() { return writer; }

        public int getWordcnt() { 
            return words!=null ? words.Count : 0; 
        }

        public List<Word> getWords() { return words; }    

        public BookInfo(String fileName, String title, String writer, List<Word> words)
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
            return "#\twriter\tfileName\ttitle\tword\tdefinition\twordcnt\tno\r\n";
        }

        public String ToLines(int all_seq)
        {
           StringBuilder sb=new StringBuilder();

            for (int i=0;i<words.Count;++i)
            {
                sb.Append(++all_seq).Append("\t");
                sb.Append(writer).Append("\t");
                sb.Append(fileName).Append("\t");
                sb.Append(title).Append("\t");
                
                sb.Append(words[i].getEng()).Append("\t");
                sb.Append(words[i].getDefinition()).Append("\t");
                sb.Append(words.Count).Append("\t");
                sb.Append(i + 1);

                sb.Append("\r\n");             
             
            }

            return sb.ToString();
        }
    }
}
