using AutoMapper;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Mappers;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.UserFullName, opt =>
                opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
    }
}

