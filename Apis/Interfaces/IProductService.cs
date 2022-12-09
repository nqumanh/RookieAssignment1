using Apis.Models;
using Apis.QueryParameters;
using SharedViewModels;

namespace Apis.Interface;

public interface IProductService
{
    Task<PagedResponseModel<ProductDTO>> GetProductsAsync(ProductParameters productParameters);
}
