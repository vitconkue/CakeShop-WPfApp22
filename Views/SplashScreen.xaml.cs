using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CakeShop_WPfApp.Services;
using CakeShop_WPfApp.Models;
namespace CakeShop_WPfApp.Views
{

    public partial class SplashScreen : Window
    {
        Timer timer;
        double count = 0;
        double time = 5;
        Random rng = new Random();
        private List<CakeModel> CakeList;
        private CakeServices CakeServices = new CakeServices();
  
        public SplashScreen()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);

            if (showSplash == false)
            {
                var screen = new MainWindow();
                screen.Show();
                this.Close();
            }
            else
            {
                CakeList = CakeServices.GetAllCakes();
                int len = CakeList.Count();
            Rerandom: int index = rng.Next(0, len - 1);
                info.Text = CakeList[index].Information;
                CakeImage.Source = new BitmapImage(CakeList[index].ImageLink) ;
                if (info.Text == "")
                    goto Rerandom;
            
                timer = new Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Interval = 10;
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count = count + 0.01;
            if (count.CompareTo(time) == 1)
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    var screen = new MainWindow();
                    screen.Show();
                    this.Close();
                });

            }

            Dispatcher.Invoke(() =>
            {
                progress.Value = count;
            });
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }


        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            var screen = new MainWindow();
            screen.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
