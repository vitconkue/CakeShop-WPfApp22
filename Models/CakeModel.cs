﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.Models
{
    public class CakeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }


        public int ImportPrice { get; set; }
        public int SellingPrice { get; set; }
        public int Amount { get; set; }

        public int CategoryID { get; set; }
        public string Unit { get; set; }
        public string Information { get; set; }

     

        public void ShowConsole()
        {
            Console.WriteLine($"Cake ID: {ID} , Cake name: {Name} , Unit: {Unit}");
        }
    }
}
