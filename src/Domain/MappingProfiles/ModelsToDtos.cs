using System.Linq;
using AutoMapper;
using src.Domain.DTOs.Token;
using src.Domain.DTOs.Users;
using src.Domain.Models.Users;
using src.Infrastructure.Security;

namespace src.Domain.MappingProfiles
{
    public class ModelsToDtos : Profile
    {
        public ModelsToDtos()
        {
            CreateMap<User, UserDtos>()
                .ForMember(u => u.Roles, options => options.MapFrom(u => 
                    u.UserRoles.Select(ur => ur.Role.Name)));

            CreateMap<AccessToken, TokenDtos>()
                .ForMember(a => a.AccessToken, options => options.MapFrom(a => a.Token))
                .ForMember(a => a.RefreshToken, options => options.MapFrom(a => a.RefreshToken.Token))
                .ForMember(a => a.Expiration, options => options.MapFrom(a => a.Expiration));
        }
    }
}