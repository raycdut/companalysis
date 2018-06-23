using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._1CompanyImportTool.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string StockNum { get; set; }
        public string StockName { get; set; }
        public DateTime IPODate { get; set; }
        public decimal TotalEquity { get; set; }
        public decimal CirculatingEquity { get; set; }

    }
}
