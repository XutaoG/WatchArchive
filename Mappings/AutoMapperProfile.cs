using AutoMapper;
using WatchArchive.Server.DTOs.RequestDTOs;
using WatchArchive.Server.DTOs.ResponseDTOs;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserResponse>();

        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();
    }
}
