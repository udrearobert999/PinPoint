using Domain.Core;
using Domain.Entities;
using Infrastructure.Persistence;
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

        services.AddDefaultIdentity<User, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<PinPointContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IEmailSender, EmailSenderService>();
        services.AddTransient<IUserTwoFactorTokenProvider<IdentityUser>, EmailTokenProvider<IdentityUser>>();

        return services;
    }
}