using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeShop_WPfApp.Database;
using CakeShop_WPfApp.Models;
using Dapper;


namespace CakeShop_WPfApp.Services
{
    public interface ICategoryServices
    {
        CategoryModel LoadSingleCategory(int IdToLoad);
        bool AddCategory(CategoryModel category); // pass model without ID

        List<CategoryModel> LoadAll();
    }
    class CategoryServices : ICategoryServices
    {
        private string _connectionString = DatabaseAccess.LoadConnectionString();

        public bool AddCategory(CategoryModel category)
        {
            string sqlInsertString = $"INSERT INTO CATEGORY(NAME) VALUES (@Name)";
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                var output = cnn.Execute(sqlInsertString, category);
                if (output == 1)
                    return true;
                return false;
            }
        }

        public List<CategoryModel> LoadAll()
        {
            List<CategoryModel> result = new List<CategoryModel>();
            string sqlString = $"SELECT * FROM CATEGORY";
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                result = cnn.Query<CategoryModel>(sqlString, new DynamicParameters()).ToList();
            }
            return result;
        }

        public CategoryModel LoadSingleCategory(int IdToLoad)
        {
            CategoryModel result = new CategoryModel();
            string sqlString = $"SELECT * FROM CATEGORY WHERE ID = {IdToLoad}";
            using (var cnn = new SQLiteConnection(_connectionString))
            {
                var output = cnn.QueryFirst<CategoryModel>(sqlString, new DynamicParameters());

                result = output;
               

            }
            return result;
        }
    }
}
