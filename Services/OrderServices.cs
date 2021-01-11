using CakeShop_WPfApp.Database;
using CakeShop_WPfApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CakeShop_WPfApp.Services
{
    public interface IOrderServices
    {
         OrderModel LoadSingleOrder(int orderID);
        bool AddOrder(OrderModel orderModel);

        List<OrderModel> LoadAllOrder(); 
    }
    class OrderServices: IOrderServices
    {
        private string _connectionString = DatabaseAccess.LoadConnectionString();
        public bool AddOrder(OrderModel orderModel)
        {
            throw new NotImplementedException();
        }

        public List<OrderModel> LoadAllOrder()
        {
            throw new NotImplementedException();
        }

        public OrderModel LoadSingleOrder(int orderID)
        {
            CakeServices cakeServices = new CakeServices();
            OrderModel result = new OrderModel();
            string sqlString = $"SELECT * FROM BILL WHERE ID = {orderID}";
            var cakesInOrderList = new List<CakeInOrder>();
            var cakesIDList = new List<int>();
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                // load order information
                result = cnn.QueryFirst<OrderModel>(sqlString, new DynamicParameters());
                // load list cake in order
                cakesInOrderList = cnn.Query<CakeInOrder>($"SELECT * FROM CAKEINORDER WHERE ORDERID = {orderID}", new DynamicParameters())
                    .ToList();

                foreach (var x in cakesInOrderList)
                {
                    x.cake = cakeServices.loadSingleCake(x.CakeID);
                }

                result.listCakes = cakesInOrderList;




            }

            return result;
        }

      
    }
}
