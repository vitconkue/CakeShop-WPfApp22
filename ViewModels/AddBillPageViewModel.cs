using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.ViewModels
{
    public class AddBillPageViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        public AddBillPageViewModel(MainViewModel param)
        {
            mainViewModel = param;
        }
    }
}
