using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.Models
{
    public class CakeInOrder
    {
        public int OrderID { get; set; }
        public CakeModel cake { get; set; }

        public int CakeID { get; set; }
        public int Amount { get; set; }
        public int SellPrice { get; set; }

        public int CalculateSum()
        {
            return Amount * SellPrice;
        }
        public int TotalMoney()
        {
            return CalculateSum();
        }
    }
}
