using Application.AutoMapper;
using Application.Interfaces;
using Application.Services;
using Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddTransient<ITestService, TestService>();
        services.AddTransient<IPinsService, PinsService>();
        services.AddTransient<ICommentsService, CommentsService>();

        return services;
    }
}