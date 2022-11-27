using Apis.Data;
using Apis.Interface;

namespace Apis.Repository;

public class RepositoryWrapper : IRepositoryWrapper
{
    // private BookStoreContext _context;
    // private ICategoryRepository _category;
    // private IProductRepository _product;

    // public ICategoryRepository Category
    // {
    //     get
    //     {
    //         if (_category == null)
    //         {
    //             _category = new CategoryRepository(_context);
    //         }
    //         return _category;
    //     }
    // }
    // public IProductRepository Product
    // {
    //     get
    //     {
    //         if (_product == null)
    //         {
    //             _product = new ProductRepository(_context);
    //         }
    //         return _product;
    //     }
    // }
    // public RepositoryWrapper(BookStoreContext context)
    // {
    //     _context = context;
    // }
    // public void Save()
    // {
    //     _context.SaveChanges();
    // }
}