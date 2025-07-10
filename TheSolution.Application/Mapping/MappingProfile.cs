using AutoMapper;
using TheSolution.Application.DTO;
using TheSolution.Domain.Entities;

namespace TheSolution.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(u => u.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(u => u.Pathronomic, opt => opt.MapFrom(src => src.Pathronomic))
                .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(u => u.Login, opt => opt.MapFrom(src => src.UserName))
                .ForMember(u => u.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();

        }
    }
}
