using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<PinDto, Pin>();
        CreateMap<IFormFile, byte[]>()
            .ConvertUsing(file => FileToByteArray(file) ?? Array.Empty<byte>());
    }

    private byte[]? FileToByteArray(IFormFile? file)
    {
        if (file == null)
            return null;

        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}