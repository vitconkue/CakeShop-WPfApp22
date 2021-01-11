using CakeShop_WPfApp.Services;
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

        public CakeServices _cakeServices = new CakeServices();
        public int CalculateSum()
        {
            int result = 0; 
            foreach(var x in listCakes)
            {
                result += x.CalculateSum();
            }
            return result;

        }

        public CakeInOrder AddCake(CakeModel cake, int numberOfCake)
        {
            CakeInOrder cakeInOrder = new CakeInOrder
            {
                CakeID = cake.ID,
                SellPrice = cake.SellingPrice,
                Amount = numberOfCake,
                cake = _cakeServices.loadSingleCake(cake.ID)
            };
            listCakes.Add(cakeInOrder);

            return cakeInOrder;

            
            
        }
        public OrderModel()
        {
            listCakes = new List<CakeInOrder>();
        }
    }
}
