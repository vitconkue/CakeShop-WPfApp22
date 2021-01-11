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
using CakeShop_WPfApp.Helper;

namespace CakeShop_WPfApp.Services
{
    public interface ICakeService
    {
        List<CakeModel> GetAllCakes();

        CakeModel loadSingleCake(int IdToLoad);
        bool addCake(CakeModel cakeModel); // without ID, unit is one of 'Cái', 'Hộp', 'Lốc'

        bool deleteCake(int IdToDelete);

        bool updateCakeInformationInDatabase(CakeModel changedTo); // pass in the cake model with changed information

        List<CakeModel> SearchCake(string keyword, int cagetory = -1);

        List<CakeModel> GetCakeWithPageInfo(int pageNumber, int cakePerpage, int categoryID = -1);

        int GetCount(int categoryID = -1);
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
        public bool checkOutOfCake(int cakeId)
        {
            int currentAmount = 0;

            string sqlstring = $"SELECT AMOUNT FROM CAKE WHERE ID = {cakeId}";
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                currentAmount = cnn.QueryFirst<int>(sqlstring, new DynamicParameters()); 
            }
            return currentAmount <= 0; 
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

        public List<CakeModel> SearchCake(string keyword, int cagetory = -1)
        {
            // load id-name pair 
            List<CakeModel> result = new List<CakeModel>();
            List<(int, string)> keyvaluePair = new List<(int, string)>();
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                string sqlString = "SELECT ID,NAME FROM CAKE";

                keyvaluePair = cnn.Query<(int, string)>(sqlString, new DynamicParameters()).ToList();
            }

            var filteredCakeName = keyvaluePair.Where(r => r.Item2.ToLower().Contains(keyword.ToLower()) ||
                  HelperFunctions.RemovedUTF(r.Item2.ToLower()).Contains(HelperFunctions.RemovedUTF(keyword.ToLower()))).ToList();
            filteredCakeName = filteredCakeName.OrderByDescending(r => HelperFunctions.rateSearchResult(keyword, r.Item2)).ToList();

            foreach(var x in filteredCakeName)
            {
                result.Add(loadSingleCake(x.Item1));
            }
            return result;
        }

        public List<CakeModel> GetCakeWithPageInfo(int pageNumber, int cakePerpage, int categoryID = -1)
        {
            List<CakeModel> result = new List<CakeModel>();
            List<CakeModel> allCakes = new List<CakeModel>();

            allCakes = GetAllCakes();

            result = allCakes.Skip((pageNumber - 1) * cakePerpage).Take(cakePerpage).ToList();

            return result; 
        }

        public int GetCount(int categoryID = -1)
        {
            string sqlString = "SELECT COUNT(*) FROM CAKE";
            int result = 0; 
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                result = cnn.QueryFirst<int>(sqlString, new DynamicParameters());
            }
            return result;
        }
    }
}
