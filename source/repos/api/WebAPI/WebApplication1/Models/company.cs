using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class company
    {
        public int company_code { get; set; }
        public string company_name { get; set; }
        public int turnover { get; set; }
        public string ceo { get; set; }
        public string board_of_director { get; set; }
        public string listed_in_stock_exchange { get; set; }
        public string sector_name { get; set; }
        public string brief_writeup { get; set; }
        public string stock_code { get; set; }


    }
}