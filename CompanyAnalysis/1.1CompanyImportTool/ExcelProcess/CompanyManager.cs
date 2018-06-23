using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1._1CompanyImportTool.Models;
using ClosedXML;
using ClosedXML.Excel;
using CsvHelper;
using ExcelDataReader;

namespace _1._1CompanyImportTool.ExcelProcess
{
    public class CompanyManager
    {
        public void Import()
        {
            var lstFiles = GetFiles();
            foreach (var fileName in lstFiles)
            {
                Console.WriteLine("process "+ fileName);
                ReadDataFromExcel(fileName);
            }
        }

        private List<string> GetFiles()
        {
            DirectoryInfo di = new DirectoryInfo(@"e:\datasource\sh");
            return di.GetFiles().Select(i => i.FullName).ToList();
        }

        private void ReadDataFromExcel(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {

                using (var reader = ExcelReaderFactory.CreateCsvReader(stream
                    , new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = Encoding.GetEncoding(936),
                        AutodetectSeparators = new char[] { ',', ';', '\t', '|', '#' }
                    }
                    ))
                {

                    // 2. Use the AsDataSet extension method
                    var result = reader.AsDataSet();
                    if (result.Tables.Count == 0) return;
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        int value;
                        if (!int.TryParse(row[0].ToString(), out value)) continue;

                        var company = new Company()
                        {
                            StockNum = row[0].ToString(),
                            CompanyName = row[1].ToString(),
                            StockName = row[3].ToString(),
                            //IPODate = DateTime.Parse(row[4].ToString()),
                            TotalEquity = decimal.Parse(row[5].ToString()),
                            CirculatingEquity = decimal.Parse(row[6].ToString())

                        };

                        var date = new DateTime(1980,1,1);
                        if (DateTime.TryParse(row[4].ToString(), out date))
                        {
                           
                        }

                        var tmpDate = new DateTime(1980, 1, 1);
                        if (date < tmpDate)
                        {
                            date = tmpDate;
                        }
                        company.IPODate = date;

                        Console.WriteLine("Adding " + company.CompanyName);
                        SaveCompanyInfo(company);

                    }


                }
            }
        }

        private static void SaveCompanyInfo(Company company)
        {
            using (Stocks ctx = new Stocks())
            {
                var co = ctx.Companies.SingleOrDefault(c => c.StockNum == company.StockNum);
                if (co == null)
                {
                    ctx.Companies.Add(company);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
