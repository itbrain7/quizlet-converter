
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet_converter
{
    internal interface Printable
    {
        void printLn(String msg);

        void errorLn(String msg);

        void reportProgress(int percent);
    }

}
