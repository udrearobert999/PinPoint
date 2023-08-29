using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<PinDto, Pin>().ReverseMap().ForMember(dest => dest.PictureUpload, opts => opts.Ignore());
        CreateMap<PinComment, CommentDto>().ReverseMap();
    }
}