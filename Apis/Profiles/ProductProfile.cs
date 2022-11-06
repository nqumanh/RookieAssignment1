using Apis.Models;
using AutoMapper;
using SharedViewModels;

namespace Apis.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}