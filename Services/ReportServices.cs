using CakeShop_WPfApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop_WPfApp.Services
{
    public interface IReportServices
    {
        List<int> GetMonthlyRevenue(int year);
        List<int> GetMonthlyProfit(int year);
    }
    public class ReportServices : IReportServices
    {
        private OrderServices orderServices = new OrderServices();
        public List<int> GetMonthlyRevenue(int year)
        {
            List<int> result = new List<int>();
            List<OrderModel> yearOrders = orderServices.LoadAllOrderInYear(year);
            // Initialize
            for(int i =1; i <=12; i++ )
            {
                result.Add(0);
            }

            // calculate each month
            for(int i = 1; i <= 12; i++)
            {
                foreach(var order in yearOrders)
                {
                    if(DateTime.ParseExact(order.Date, "dd-mm-yyyy", CultureInfo.InvariantCulture).Month == i)
                    {
                        result[i - 1] += order.CalculateSum();
                    }
                }
            }
            return result;
        }
        public List<int> GetMonthlyProfit(int year)
        {
            throw new NotImplementedException();
        }

        
    }
}
