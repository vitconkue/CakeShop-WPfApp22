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
        private SeriesCollection revenueData;

        public SeriesCollection RevenueData
        {
            get
            {
                return revenueData;
            }
            set
            {
                revenueData = value;
                OnPropertyChanged(nameof(RevenueData));
            }
        }
        private SeriesCollection categoryData;
        public SeriesCollection CategoryData
        {
            get
            {
                return categoryData;
            }
            set
            {
                categoryData = value;
                OnPropertyChanged(nameof(CategoryData));
            }
        }
        public OrderServices OrderServices = new OrderServices();
        public List<int> YearList { get; set; }
        private int currentYear;
        public int CurrentYear
        {
            get
            {
                return currentYear;
            }
            set
            {
                currentYear = value;
                var data = ReportServices.GetMonthlyRevenue(CurrentYear);
                InitRevenueData(data, CurrentYear);
                OnPropertyChanged(nameof(CurrentYear));
            }
        }
        public ChartPageViewModel()
        {
            YearList = OrderServices.GetAllYearWithRevenue();
            CurrentYear = YearList[0];
            var data = ReportServices.GetMonthlyRevenue(CurrentYear);
            var caterGoryRevenue = ReportServices.GetCategoryRevenue();
            InitRevenueData(data, CurrentYear);
            InitCategoryData(caterGoryRevenue);
        }
        public void InitRevenueData(List<int> data, int year)
        {
            RevenueData = new SeriesCollection { };

            RevenueData.Add(
                new ColumnSeries
                {

                    Values = new ChartValues<double> { data[0], data[1], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11] },
                    Title = $"Doanh thu từng tháng trong năm {year}"
                });
        }

        public void InitCategoryData(List<(string, int)> data)
        {
            CategoryData = new SeriesCollection { };
            foreach (var element in data)
            {
                CategoryData.Add(
                    new PieSeries
                    {
                        Title = element.Item1,
                        Values = new ChartValues<int> { element.Item2 },
                        DataLabels = true
                    });

            }
        }
        public List<string> Months
        {
            get
            {
                string[] allMonths = { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
                return new List<string>(allMonths);
            }
        }

    }
}
