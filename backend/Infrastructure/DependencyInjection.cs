using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Identity;
using Backend.Infrastructure.WebSockets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Infrastructure;

public static class DependencyInjection
{
    public static void ConfigureWebsocketServices(this IServiceCollection services)
    {
        services.AddSingleton(
            typeof(IEntityNotificationsService<Address>),
            typeof(EntityNotificationHubOperator<Address>)
        );
        services.AddSingleton(
            typeof(IEntityNotificationsService<Chief>),
            typeof(EntityNotificationHubOperator<Chief>)
        );
        services.AddSingleton(
            typeof(IEntityNotificationsService<Contract>),
            typeof(EntityNotificationHubOperator<Contract>)
        );
        services.AddSingleton(
            typeof(IEntityNotificationsService<Customer>),
            typeof(EntityNotificationHubOperator<Customer>)
        );
        services.AddSingleton(
            typeof(IEntityNotificationsService<Order>),
            typeof(EntityNotificationHubOperator<Order>)
        );
        services.AddSingleton(
            typeof(IEntityNotificationsService<Product>),
            typeof(EntityNotificationHubOperator<Product>)
        );
        services.AddSingleton(
            typeof(IEntityNotificationsService<Workshop>),
            typeof(EntityNotificationHubOperator<Workshop>)
        );

        services.AddSignalR(hubOptions => { hubOptions.HandshakeTimeout = TimeSpan.FromMinutes(30); });
    }

    public static void ConfigureAuthenticationAndAuthorization(
        this IServiceCollection services,
        string issuer,
        string audience
    )
    {
        services
            .AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            File.ReadAllBytes("/run/secrets/jwt-key")
                        ),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                }
            );

        services.AddScoped<ITokenService, TokenService>();
        services.AddAuthorization();
    }

    public static void SetupDatabaseConnection(
        this IServiceCollection services,
        string connectionStringTemplate
    )
    {
        var password = File.ReadAllText("/run/secrets/db-password");
        var connection = connectionStringTemplate.Replace("{password}", password);
        services.AddDbContext<TypographyContext>(options =>
            options.UseMySql(connection, new MySqlServerVersion(new Version(8, 2, 0)))
        );
    }
}