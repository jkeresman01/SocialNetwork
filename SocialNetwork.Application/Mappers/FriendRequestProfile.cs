using AutoMapper;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Mappers;

public class FriendRequestProfile : Profile
{
    public FriendRequestProfile()
    {
        CreateMap<FriendRequest, FriendRequestDTO>()
            .ForMember(dest => dest.SenderFullName,
                       opt => opt.MapFrom(src => $"{src.Sender.FirstName} {src.Sender.LastName}"));
    }
}

