using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeShop_WPfApp.Models;
using CakeShop_WPfApp.Database;
using Dapper;
using System.Data.SQLite;
using System.Windows;

namespace CakeShop_WPfApp.Services
{
    public interface ICakeService
    {
        List<CakeModel> GetAllCakes();

        CakeModel loadSingleCake(int IdToLoad);
        bool addCake(CakeModel cakeModel); // without ID, unit is one of 'Cái', 'Hộp', 'Lốc'

        bool deleteCake(int IdToDelete);

        bool updateCakeInformationInDatabase(CakeModel changedTo); // pass in the cake model with changed information
    }
    public class CakeServices: ICakeService
    {
        private string _connectionString = DatabaseAccess.LoadConnectionString();
        public List<CakeModel> GetAllCakes()
        {
            List<CakeModel> result = new List<CakeModel>();
         
            string sqlString = "SELECT * FROM CAKE";
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                //query 
                result = cnn.Query<CakeModel>(sqlString, new DynamicParameters()).ToList();
            }

            return result;
        }

       

        public bool addCake(CakeModel cakeModel) // without cake ID
        {
            if(cakeModel.Category.ID >= 0)
            {
                string insertSQLstring = $"INSERT INTO CAKE(NAME,IMPORTPRICE,SELLINGPRICE,AMOUNT,CATEGORYID,UNIT,INFORMATION) " +
                 $"VALUES (@Name,@ImportPrice,@SellingPrice,@Amount,{cakeModel.Category.ID},@Unit,@Information)";

                using (var cnn = new SQLiteConnection(_connectionString))
                {
                    // insert 
                    try
                    {
                        cnn.Execute(insertSQLstring, cakeModel);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
            else
            {
                // add new category
                string newCategorryName = cakeModel.Category.Name;
                var categoeyServices = new CategoryServices();

                categoeyServices.AddCategory(new CategoryModel { Name = newCategorryName });
                // load new category ID 
                string sqlLoadId = "SELECT IFNULL(MAX(ID), 0) FROM CATEGORY";

                int currentMaxCategoryId;

                using (var cnn = new SQLiteConnection(_connectionString))
                {
                    currentMaxCategoryId = cnn.QueryFirst<int>(sqlLoadId, new DynamicParameters());
                    // add cake normally
                    string insertSQLstring = $"INSERT INTO CAKE(NAME,IMPORTPRICE,SELLINGPRICE,AMOUNT,CATEGORYID,UNIT,INFORMATION) " +
                 $"VALUES (@Name,@ImportPrice,@SellingPrice,@Amount,{currentMaxCategoryId},@Unit,@Information)";
                    cnn.Execute(insertSQLstring, cakeModel);
                }
                
            }
            return true;
        }

        public CakeModel loadSingleCake(int IdToLoad)
        {
            var result = new CakeModel();
            var catetoryService = new CategoryServices();
            string sqlString = $"SELECT * FROM CAKE WHERE ID = {IdToLoad}";
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                var output = cnn.QueryFirst<CakeModel>(sqlString, new DynamicParameters());
                int categoryID = cnn.QueryFirst<int>($"SELECT CategoryID FROM CAKE WHERE ID = {IdToLoad}");
                result = output;
                var categoryService = new CategoryServices();
                result.Category = categoryService.LoadSingleCategory(categoryID);
            }

            return result;
        }

        public bool deleteCake(int IdToDelete)
        {
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                var output = cnn.Execute($"DELETE FROM CAKE WHERE ID = {IdToDelete}");

                if (output == 1)
                    return true;
                else
                    return false;
            }
        }

        public bool updateCakeInformationInDatabase(CakeModel changedTo)
        {
            string sqlUpdateString = $"UPDATE CAKE " +
                $"SET NAME = @Name, " +
                $"IMPORTPRICE = @ImportPrice, " +
                $"SELLINGPRICE  = @SellingPrice, " +
                $"AMOUNT = @Amount, " +
                $"CATEGORYID = {changedTo.Category.ID}, " +
                $"UNIT = @Unit, " +
                $"INFORMATION = @Information " +
                $"WHERE ID = {changedTo.ID}";

            using (var cnn = new SQLiteConnection(_connectionString))
            {
                var output = cnn.Execute(sqlUpdateString, changedTo);

                if (output == 1)
                    return true;
                else { return false; }
            }
        }
    }
}
