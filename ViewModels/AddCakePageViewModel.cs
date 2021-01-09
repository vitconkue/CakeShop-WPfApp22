using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.ViewModels
{

    public  class AddCakePageViewModel: BaseViewModel
    {
        private MainViewModel mainViewModel;
        public AddCakePageViewModel(MainViewModel param)
        {
            mainViewModel = param;
        }

    }
}
