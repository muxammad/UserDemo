using AutoMapper;
using UserDemo.Domain.Entities;
using UserDemo.Service.DTOs;

namespace UserDemo.Service.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
    }
}
