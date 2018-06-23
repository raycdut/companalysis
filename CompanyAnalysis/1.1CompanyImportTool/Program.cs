using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1._1CompanyImportTool.ExcelProcess;

namespace _1._1CompanyImportTool
{
    class Program
    {
        static void Main(string[] args)
        {
            new CompanyManager().Import();
        }
    }
}
