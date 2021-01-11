using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }

        public List<CakeInOrder> listCakes { get; set; }


        public int CalculateSum()
        {
            return 1; 
        }
    }
}
