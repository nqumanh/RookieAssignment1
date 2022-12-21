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
        var productQuery = _productRepository.Entities
                                .Include(x => x.Category)
                                ;

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

    public async Task<ProductDTO?> GetProductById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
            return ProductDTO(product);
        return null;
    }

    private static ProductDTO ProductDTO(Product product)
    {
        var numberOfRating = product.Ratings.Count;
        var avgRating = numberOfRating > 0 ? Convert.ToDecimal(product.Ratings.Aggregate(0, (sum, rating) => sum + rating.Star)) / numberOfRating : 0;
        var categoryId = (product.Category == null) ? null : product.Category.Id.ToString();
        var categoryName = (product.Category == null) ? null : product.Category.Name;
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Image = product.Image,
            Author = product.Author,
            AverageRating = avgRating,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = categoryId,
            CategoryName = categoryName,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate
        };
    }
}
