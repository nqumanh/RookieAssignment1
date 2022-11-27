using Apis.Models;

namespace Apis.Interface;

public interface IProductRepository : IRepositoryBase<Product>
{
    IEnumerable<Product> GetProducts(ProductParameters productParameters);
}
