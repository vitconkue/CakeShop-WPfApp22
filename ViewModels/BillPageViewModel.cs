using CakeShop_WPfApp.Commands;
using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using CakeShop_WPfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeShop_WPfApp.ViewModels
{
    public class BillPageViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        public List <OrderModel> OrderList { get; set; }
        public OrderServices OrderServices = new OrderServices();
        public ICommand GotoDetail { get; set; }
        public BillPageViewModel(MainViewModel param)
        {
            mainViewModel = param;
            OrderList = OrderServices.LoadAllOrder();
            GotoDetail = new RelayCommand(o => GotoDetailPage(o));
        }
        public void GotoDetailPage(object parameter)
        {
            var DetailWindow = new DetailBilPage(int.Parse(parameter.ToString()));
            DetailWindow.Show();
        }
    }
}
