using Backend.Infrastructure;

namespace Backend.Web.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static readonly string CorsPolicyName = "_myAllowSpecificOrigins";

        public static void AddConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.SetupCors();
            services.AddControllers().ConfigureJsonSerializationOptions();
            services.AddEndpointsApiExplorer();
            services.ConfigureWebsocketServices();
            services.ConfigureAuthenticationAndAuthorization(
                configuration["JwtSettings:Issuer"]!,
                configuration["JwtSettings:Audience"]!
            );

            string connectionStringTemplate = configuration.GetConnectionString(
                "DefaultConnection"
            )!;
            services.SetupDatabaseConnection(connectionStringTemplate);
            services.AddSwaggerGen();
        }

        private static void SetupCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: CorsPolicyName,
                    builder =>
                    {
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.WithOrigins("http://localhost", "http://frontend");
                    }
                );
            });
        }
    }
}
