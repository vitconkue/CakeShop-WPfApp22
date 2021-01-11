using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.ViewModels
{
    public class DetailBillPageViewModel:BaseViewModel
    {
        public OrderModel Order { get; set; }
        public OrderServices OrderServices = new OrderServices();
        public DetailBillPageViewModel(int id)
        {
            Order = OrderServices.LoadSingleOrder(id);
        }
    }
}
