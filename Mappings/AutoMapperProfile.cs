using AutoMapper;
using WatchArchive.Server.DTOs.RequestDTOs;
using WatchArchive.Server.DTOs.ResponseDTOs;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User
        CreateMap<User, UserResponse>();

        // Thumbnail
        CreateMap<Thumbnail, ThumbnailResponse>();

        // Category
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();

        // Category Entry
        CreateMap<CategoryEntry, CategoryEntryResponse>();

        // Tag
        CreateMap<CreateTagRequest, Tag>();
        CreateMap<UpdateTagRequest, Tag>();
        CreateMap<Tag, TagResponse>();

        // Tag Entry
        CreateMap<TagEntry, TagEntryResponse>();

        // Rated Entry
        CreateMap<RatedEntry, RatedEntryResponse>();
    }
}
