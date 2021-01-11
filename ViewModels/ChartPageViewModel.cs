using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeShop_WPfApp.Services;
using LiveCharts;
using LiveCharts.Wpf;
namespace CakeShop_WPfApp.ViewModels
{

    public class ChartPageViewModel : BaseViewModel
    {
        public ReportServices ReportServices = new ReportServices();
        public SeriesCollection RevenueData { get; set; }
        public ChartPageViewModel()
        {
            var data = ReportServices.GetMonthlyRevenue(2021);
            InitRevenueData(data, 2021);
        }
        public void InitRevenueData(List<int> data, int year)
        {
            RevenueData = new SeriesCollection { };

            RevenueData.Add(
                new ColumnSeries
                {

                    Values = new ChartValues<double> { data[0], data[1], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11] },
                    Title = $"Doanh thu trong năm {year}"
                });
        }
        public List<string> Months
        {
            get
            {
                string[] allMonths ={ "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4" , "Tháng 5" , "Tháng 6" , "Tháng 7" , "Tháng 8","Tháng 9", "Tháng 10" , "Tháng 11" ,"Tháng 12" };
                return new List<string>(allMonths);
            }
        }
    }
}
