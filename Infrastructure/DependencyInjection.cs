using Application.Interfaces;
using Application.Services;
using Domain.Core;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PinPointContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("PinPointDB"));
        });

        services.AddScoped<DbContext, PinPointContext>();

        services.AddIdentity<User, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<PinPointContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IRepository<Pin, Guid>, Repository<Pin, Guid>>();
        services.AddScoped<IReadOnlyRepository<Pin, Guid>, ReadOnlyRepository<Pin, Guid>>();
        services.AddScoped<IPinsRepository, PinsRepository>();

        services.AddScoped<IRepository<PinComment, Guid>, Repository<PinComment, Guid>>();

        services.AddTransient<IEmailSender, EmailSenderService>();
        services.AddTransient<IUserTwoFactorTokenProvider<IdentityUser>, EmailTokenProvider<IdentityUser>>();

        return services;
    }
}