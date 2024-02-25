using backend.Database;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Utilities.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static void ConfigureWebsocketServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(EntityNotificationHubOperator<Address>));
            services.AddSingleton(typeof(EntityNotificationHubOperator<Chief>));
            services.AddSingleton(typeof(EntityNotificationHubOperator<Contract>));
            services.AddSingleton(typeof(EntityNotificationHubOperator<Customer>));
            services.AddSingleton(typeof(EntityNotificationHubOperator<Order>));
            services.AddSingleton(typeof(EntityNotificationHubOperator<Product>));
            services.AddSingleton(typeof(EntityNotificationHubOperator<Workshop>));

            services.AddSignalR(hubOptions =>
            {
                hubOptions.HandshakeTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public static void ConfigureAuthenticationAndAuthorization(
            this IServiceCollection services,
            string issuer,
            string audience
        )
        {
            services
                .AddAuthentication(
                    (options) =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    (options) =>
                    {
                        options.TokenValidationParameters = new()
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

            services.AddAuthorization();
        }

        public static void SetupDatabaseConnection(
            this IServiceCollection services,
            string connectionStringTemplate
        )
        {
            string password = File.ReadAllText("/run/secrets/db-password");
            string connection = connectionStringTemplate.Replace("{password}", password);
            services.AddDbContext<TypographyContext>(options =>
                options.UseMySql(connection, new MySqlServerVersion(new Version(8, 2, 0)))
            );
        }
    }
}
