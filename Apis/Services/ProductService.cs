using Apis.Models;
using Apis.Interface;
using AutoMapper;
using Apis.QueryParameters;
using Apis.Repository;
using SharedViewModels;
using Microsoft.EntityFrameworkCore;

namespace Apis.Services;

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<PagedResponseModel<ProductDTO>> GetProductsAsync(ProductParameters productParameters)
    {
        var productQuery = _productRepository.GetWithCondition(null, null, "");

        var totalItems = productQuery.Count();

        var products = await productQuery
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToListAsync();

        var productDTOs = new List<ProductDTO>();
        if (totalItems > 0)
        {
            productDTOs = _mapper.Map<List<ProductDTO>>(products);
        }

        return new PagedResponseModel<ProductDTO>
        {
            TotalItems = totalItems,
            Items = productDTOs,
        };
    }
}
