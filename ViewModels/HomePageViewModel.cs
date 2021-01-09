using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CakeShop_WPfApp.Commands;

namespace CakeShop_WPfApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        public ICommand UpdateView { get; set; }
        public HomePageViewModel(MainViewModel param)
        {
            mainViewModel = param;
            UpdateView = new UpdateMainViewCommand(mainViewModel);
        }
    }
}
