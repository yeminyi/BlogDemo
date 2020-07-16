using AutoMapper;
using BlogDemo.Core.Entities;
using BlogDemo.Infrastructure.Resources;

namespace BlogDemo.Api.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostResource>()
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.LastModified));
            CreateMap<PostResource, Post>();
            CreateMap<PostAddResource, Post>();
            CreateMap<PostUpdateResource, Post>();

            CreateMap<PostImage, PostImageResource>();
            CreateMap<PostImageResource, PostImage>();
        }
    
    }
}

