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
                using (MySqlConnection mySqlConnection =
                 new MySqlConnection("host=localhost;port=3306;uid=root;password=root;database=product;"))
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        mySqlCommand.CommandText =
                        "Select * From product";
                        mySqlCommand.Connection = mySqlConnection;
                        if (mySqlConnection.State !=
                        System.Data.ConnectionState.Open)
                            mySqlConnection.Open();
                        using (var mySqlReader =
                         mySqlCommand.ExecuteReader())
                        {
                            while (mySqlReader.Read())
                            {
                               Product product = new Product();
                               product.Id =
                               int.Parse(mySqlReader.GetValue(0).ToString());
                               product.ProductName =
                               mySqlReader.GetValue(1).ToString();
                               product.Price = 
                               decimal.Parse(mySqlReader.GetValue(2).ToString());
                               product.Description =

                               mySqlReader.GetValue(3).ToString();
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
