using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet_converter
{
    /// <summary>
    /// 영어단어와 뜻을 가지고 있는 클래스
    /// </summary>
    class Word
    {
        String eng;

        String definition;

        public Word(string eng, string definition) 
        {
            this.eng = clean(eng, true);
            this.definition = clean(definition, false);
        }

        public String getEng()
        {
            return this.eng;
        }

        public String getDefinition()
        {
            return this.definition;
        }

        public String ToString()
        {
            return $"eng={eng}, definition={definition}";
        }

        private String clean(String str, Boolean isWord)
        {

            String newstr = str;

            if (newstr != null)
            {

                newstr = newstr.Replace("\t", " ");
                newstr = newstr.Replace("\r\n", " ");
                newstr = newstr.Replace("\r", " ");
                newstr = newstr.Replace("\n", " ");

                newstr = newstr.Replace("\t", " ");

                newstr = newstr.Replace("  ", " ");

                if (isWord)
                {
                    int idx = newstr.IndexOf("\\\\n");
                    if (idx>=0)
                    {
                        newstr = newstr.Substring(0, idx);
                    }
                }
                else
                {
                    newstr = newstr.Replace("\\\\n", "; ");                   
                }
            }

            return newstr;
        }

    }
}
