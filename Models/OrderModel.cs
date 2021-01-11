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
        private int CheckExistedCake(int cakeID)
        {
            int listleng = listCakes.Count;
            int result = -1; 
            for(int i =0; i < listleng; ++i)
            {
                if(listCakes[i].cake.ID == cakeID)
                {
                    return i;
                }
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

            int check = CheckExistedCake(cakeInOrder.CakeID);
            if (check == -1)
            {
                listCakes.Add(cakeInOrder);
            }
            else
            {
                listCakes[check].Amount++;
            }
           

            return cakeInOrder;

            
            
        }
        public OrderModel()
        {
            listCakes = new List<CakeInOrder>();
        }
    }
}
