using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodStocks1
{
   public class StockFile
    {
        // Properties of stock file
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int CurrentCount { get; set; }
        public string OnOrder { get; set; }
    }
}
