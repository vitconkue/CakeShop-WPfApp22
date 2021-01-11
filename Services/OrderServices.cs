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
            // add to order table 
            bool result = false;
            int currentMaxID;
            string sqlInsertString = $"INSERT INTO BILL(DATE,CUSTOMERNAME,CUSTOMERPHONE) VALUES (@Date, @CustomerName, @CustomerPhone)";
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                int insertResult = cnn.Execute(sqlInsertString, orderModel);
                result = (insertResult == 1);

                // get newly added orderID
                currentMaxID = cnn.QueryFirst<int>("SELECT IFNULL(MAX(ID), 0) FROM BILL");
            }



            // Add to cakeinorder table
            List<CakeInOrder> listCakeOrders = orderModel.listCakes;
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                foreach (var x in listCakeOrders)
                {
                    string sqlAddCakeInOrder = $"INSERT INTO CAKEINORDER VALUES ({currentMaxID},{x.cake.ID},{x.SellPrice},{x.Amount})";
                    cnn.Execute(sqlAddCakeInOrder);
                }
            }
            return result;
        }

        public List<OrderModel> LoadAllOrder()
        {
            CakeServices cakeServices = new CakeServices();

            List<OrderModel> result = new List<OrderModel>();
            List<int> listOrderID = new List<int>();
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                // load all orderid 

                listOrderID = cnn.Query<int>("SELECT ID FROM BILL").ToList();
            }
            foreach (var orderId in listOrderID)
            {
                result.Add(LoadSingleOrder(orderId));
            }

            return result;
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
