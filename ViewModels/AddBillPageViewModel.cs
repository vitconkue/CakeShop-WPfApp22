using CakeShop_WPfApp.Commands;
using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CakeShop_WPfApp.ViewModels
{
    public class AddBillPageViewModel : BaseViewModel
    {
        public CakeServices CakeServices = new CakeServices();
        public List<CakeModel> CakeList { get; set; }
        private OrderModel _order;
        public ICommand AddToBill { get; set; }
        public ICommand DeleteCakeInOrder {get; set; }
        public ICommand IncreaseAmount { get; set; }
        public ICommand DecreaseAmount { get; set; }
        public OrderModel Order
        {
            get;
            set;
        }
        private ObservableCollection<CakeInOrder> cakeInOrders;
        public ObservableCollection<CakeInOrder> CakeInOrders
        {
            set
            {
                cakeInOrders = value;
            }
            get
            {
                return cakeInOrders;
                OnPropertyChanged(nameof(CakeInOrders));
            }
        }
        private MainViewModel mainViewModel;
        public AddBillPageViewModel(MainViewModel param)
        {
            mainViewModel = param;
            CakeList = CakeServices.GetAllCakes();
            Order = new OrderModel();
            CakeInOrders = new ObservableCollection<CakeInOrder>();
            CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes);
            AddToBill = new RelayCommand(o => AddCakeToBill(o));
            DeleteCakeInOrder = new RelayCommand(o => DeleteCake(o));
            IncreaseAmount = new RelayCommand(o => IncreseAmountInOrder(o));
            DecreaseAmount = new RelayCommand(o => DecreseAmountInOrder(o));
        }
        public void AddCakeToBill(object parameter)
        {

            var Cake = CakeServices.loadSingleCake(int.Parse(parameter.ToString()));
            Order.AddCake(Cake, 1);
            CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes); 
            OnPropertyChanged(nameof(CakeInOrders));

        }
        public void DeleteCake(object parameter)
        {
            var id = int.Parse(parameter.ToString());
            Order.DeleteFromOrder(id);
            CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes);
            OnPropertyChanged(nameof(CakeInOrders));
        }
        public void IncreseAmountInOrder(object parameter)
        {
            var id = int.Parse(parameter.ToString());
            bool result=Order.IncreaseCakeAmount(id);
            if (result)
            {
                CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes);
                OnPropertyChanged(nameof(CakeInOrders));
            }
            else
            {
                MessageBox.Show("Không đủ bánh trong kho!!");
            }
        }
        public void DecreseAmountInOrder(object parameter)
        {
            var id = int.Parse(parameter.ToString());
            Order.DecreaseCakeAmount(id);
            CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes);
            OnPropertyChanged(nameof(CakeInOrders));
        }
    }
}
