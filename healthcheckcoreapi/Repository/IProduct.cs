using healthcheckcoreapi.Models;

namespace healthcheckcoreapi.Repository
{
    public interface IProduct
    {
        public List<Product> GetProducts();
    }
}
