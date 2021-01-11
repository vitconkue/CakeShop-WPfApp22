using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.ViewModels
{
    public class BillPageViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        public List <OrderModel> OrderList { get; set; }
        public OrderServices OrderServices = new OrderServices();
        public BillPageViewModel(MainViewModel param)
        {
            mainViewModel = param;
            OrderList = OrderServices.LoadAllOrder();
        }
    }
}
