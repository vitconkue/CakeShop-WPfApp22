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
        public ICommand AddOrderToDB { get; set; }
        public ICommand RefreshPage { get; set; }
        public OrderServices OrderServices = new OrderServices();
        private int totalPrice;
        public int TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
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
            AddOrderToDB = new RelayCommand(o => AddNewOrderToDB(o));
            RefreshPage = new RelayCommand(o => RefreshPageInfo());
            TotalPrice = 0;
        }
        public void AddCakeToBill(object parameter)
        {

            var Cake = CakeServices.loadSingleCake(int.Parse(parameter.ToString()));
            Order.AddCake(Cake, 1);
            CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes); 
            OnPropertyChanged(nameof(CakeInOrders));
            TotalPrice = Order.CalculateSum();

        }
        public void DeleteCake(object parameter)
        {
            var id = int.Parse(parameter.ToString());
            Order.DeleteFromOrder(id);
            CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes);
            OnPropertyChanged(nameof(CakeInOrders));
            TotalPrice = Order.CalculateSum();
        }
        public void IncreseAmountInOrder(object parameter)
        {
            var id = int.Parse(parameter.ToString());
            bool result=Order.IncreaseCakeAmount(id);
            if (result)
            {
                CakeInOrders = new ObservableCollection<CakeInOrder>(Order.listCakes);
                OnPropertyChanged(nameof(CakeInOrders));
                TotalPrice = Order.CalculateSum();
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
            TotalPrice = Order.CalculateSum();
        }
        public void AddNewOrderToDB(object parameter)
        {
            var tokens = (object[])parameter;
            var CustomerName = tokens[0].ToString();
            var CustomerPhone = tokens[1].ToString();
            if (CustomerName == "" || CustomerPhone == "")
            {
                MessageBox.Show("Số điện và tên khách hàng không được trống");
            }
            else
            {
                var date =  DateTime.Now.ToString("dd-MM-yyyy");
                Order.Date = date;
                Order.CustomerName = CustomerName;
                Order.CustomerPhone = CustomerPhone;
                OrderServices.AddOrder(Order);
                MessageBox.Show("Đã thêm đơn hàng mới");
            }
        }
        public void RefreshPageInfo()
        {
            mainViewModel.SelectedViewModel = new AddBillPageViewModel(mainViewModel);
        }
    }
}
