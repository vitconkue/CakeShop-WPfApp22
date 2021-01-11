﻿using CakeShop_WPfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace CakeShop_WPfApp.Views
{
    /// <summary>
    /// Interaction logic for DetailBilPage.xaml
    /// </summary>
    public partial class DetailBilPage : Window
    {
        public DetailBilPage(int id)
        {
            InitializeComponent();
            DataContext = new DetailBillPageViewModel(id);

        }

       
    }
}
