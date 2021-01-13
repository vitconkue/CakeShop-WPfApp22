using CakeShop_WPfApp.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CakeShop_WPfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

     

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            HomeIcon.Foreground = Brushes.White;
            HomeTitle.Foreground = Brushes.White;
            BillIcon.Foreground = Brushes.White;
            BillTitle.Foreground = Brushes.White;
            ChartIcon.Foreground = Brushes.Yellow;
            ChartTitle.Foreground = Brushes.Yellow;
        }

        private void BillButton_Click(object sender, RoutedEventArgs e)
        {
            HomeIcon.Foreground = Brushes.White;
            HomeTitle.Foreground = Brushes.White;
            BillIcon.Foreground = Brushes.Yellow;
            BillTitle.Foreground = Brushes.Yellow;
            ChartIcon.Foreground = Brushes.White;
            ChartTitle.Foreground = Brushes.White;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeIcon.Foreground = Brushes.Yellow;
            HomeTitle.Foreground = Brushes.Yellow;
            BillIcon.Foreground = Brushes.White;
            BillTitle.Foreground = Brushes.White;
            ChartIcon.Foreground = Brushes.White;
            ChartTitle.Foreground = Brushes.White;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
