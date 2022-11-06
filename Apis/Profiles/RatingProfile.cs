using Apis.Models;
using AutoMapper;
using SharedViewModels;

namespace Apis.Profiles;

public class RatingProfile : Profile
{
    public RatingProfile()
    {
        CreateMap<Rating, RatingDTO>();
    }
}