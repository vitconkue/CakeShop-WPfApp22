using CakeShop_WPfApp.Commands;
using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeShop_WPfApp.ViewModels
{
    public class AddBillPageViewModel : BaseViewModel
    {
        public CakeServices CakeServices = new CakeServices();
        public List<CakeModel> CakeList { get; set; }
        private ObservableCollection<CakeInOrder> _cakeInOrders;
        public ICommand AddToBill { get; set; }
        public ObservableCollection<CakeInOrder> CakeInOrders
        {
            get
            {
                return _cakeInOrders;
            }
            set
            {
                _cakeInOrders = value;
                OnPropertyChanged(nameof(CakeInOrders));
            }
        }
        private MainViewModel mainViewModel;
        public AddBillPageViewModel(MainViewModel param)
        {
            mainViewModel = param;
            CakeList = CakeServices.GetAllCakes();
            CakeInOrders = new ObservableCollection<CakeInOrder>();
            AddToBill = new RelayCommand(o => AddCakeToBill(o));
        }
        public void AddCakeToBill(object parameter){

            var Cake = CakeServices.loadSingleCake(int.Parse(parameter.ToString()));
            var CakeOrder = new CakeInOrder();
            CakeOrder.cake = Cake;
            CakeOrder.Amount = 1;
            CakeOrder.SellPrice = Cake.SellingPrice;
            CakeInOrders.Add(CakeOrder);
            OnPropertyChanged(nameof(CakeInOrders));
        }
        public bool CheckIsExistInOrder(int id)
        {

            return false;
        }
    }
}
