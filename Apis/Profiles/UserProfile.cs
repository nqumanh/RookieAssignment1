using Apis.Models;
using AutoMapper;
using SharedViewModels;

namespace Apis.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
    }
}