using Apis.Data;
using Apis.Interface;
using Apis.Models;

namespace Apis.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(BookStoreContext context)
        : base(context)
    {
    }

    public IEnumerable<Product> GetProducts(ProductParameters productParameters)
    {
        return FindAll()
            .OrderBy(on => on.Id)
            .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
            .Take(productParameters.PageSize)
            .ToList();
    }
}