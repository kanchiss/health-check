using healthcheckcoreapi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace healthcheckcoreapi.Repository
{
    public class ProductService : IProduct
    {
        public List<Product> GetProducts()
        {
            try
            {
                List<Product> products = new List<Product>();
                using (MySqlConnection pgSqlConnection =
                 new MySqlConnection("User Id = postgres; Password = sa123#;" +
                 "host=localhost;database=Demo;"))
                {
                    using (MySqlCommand pgSqlCommand = new MySqlCommand())
                    {
                        pgSqlCommand.CommandText =
                        "Select * From product";
                        pgSqlCommand.Connection = pgSqlConnection;
                        if (pgSqlConnection.State !=
                        System.Data.ConnectionState.Open)
                            pgSqlConnection.Open();
                        using (var pgSqlReader =
                         pgSqlCommand.ExecuteReader())
                        {
                            while (pgSqlReader.Read())
                            {
                               Product product = new Product();
                               product.Id =
                               int.Parse(pgSqlReader.GetValue(0).ToString());
                               product.ProductName =
                               pgSqlReader.GetValue(1).ToString();
                               product.Price = 
                               decimal.Parse(pgSqlReader.GetValue(2).ToString());
                               product.Description =

                               pgSqlReader.GetValue(3).ToString();
                               products.Add(product);
                            }
                        }
                    }
                }
                return products;
            }
            catch
            {
                throw;
            }
        }
    }
}
