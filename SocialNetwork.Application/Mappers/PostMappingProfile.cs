using AutoMapper;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Payloads;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Mappers;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostDTO>()
            .ForMember(dest => dest.UserFullName, opt =>
                opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

        CreateMap<CreatePostRequest, Post>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}

