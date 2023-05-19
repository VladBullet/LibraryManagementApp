using AutoMapper;
using LibraryManagementApi.Data.Models;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Models;
using System.Net;

namespace LibraryManagementApi.Helpers_Extensions
{
    public class AutomapperProfiles
    {
    }
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
