using Microsoft.OpenApi.Models;

namespace ERPSystem.Web.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(Options =>
            {
                Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Enter'Bearer' Followed by Space and Then Put you Token",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                Options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] {}
                    }
                });
            });
            return Services;
        }
    }
}