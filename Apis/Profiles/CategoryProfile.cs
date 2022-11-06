using Apis.Models;
using AutoMapper;
using SharedViewModels;

namespace Apis.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDTO>();
    }
}