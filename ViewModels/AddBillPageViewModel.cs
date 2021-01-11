﻿using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.ViewModels
{
    public class AddBillPageViewModel : BaseViewModel
    {
        public CakeServices CakeServices = new CakeServices();
        public List<CakeModel> CakeList { get; set; }
        private MainViewModel mainViewModel;
        public AddBillPageViewModel(MainViewModel param)
        {
            mainViewModel = param;
            CakeList = CakeServices.GetAllCakes();
        }
    }
}
