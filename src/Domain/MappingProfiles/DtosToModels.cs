using AutoMapper;
using src.Domain.DTOs.Users;
using src.Domain.Models.Users;

namespace src.Domain.MappingProfiles
{
    public class DtosToModels : Profile
    {
        public DtosToModels()
        {
            CreateMap<UserCredentialsDtos, User>();
        }
    }
}