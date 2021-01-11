using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.ViewModels
{
    public class CakeDetailPageViewModel : BaseViewModel
    {
        public CakeModel Cake { get; set; }
        public CakeServices CakeServices = new CakeServices();
        public string Title { get; set; }
        public CakeDetailPageViewModel(int cakeID)
        {
            //Cake = CakeServices.GetCakeWithID(cakeID);
            Title = $"Bánh {cakeID}";
        }
    }
}
