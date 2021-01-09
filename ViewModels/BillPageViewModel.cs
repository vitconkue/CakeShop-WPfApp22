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
        public BillPageViewModel(MainViewModel param)
        {
            mainViewModel = param;
        }
    }
}
