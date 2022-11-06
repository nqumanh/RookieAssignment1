using Apis.Models;
using AutoMapper;
using SharedViewModels;

namespace Apis.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDTO>();
    }
}