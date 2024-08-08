using Microsoft.OpenApi.Models;

namespace MovieWeb.WebApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
           => services.AddSwaggerGen(s =>
           {
               s.SwaggerDoc("v1", new OpenApiInfo
               {
                   Version = "v1",
                   Title = "User management",
                   Description = "My Api",
                   Contact = new OpenApiContact
                   {
                       Name = "Trần Đức Lộc ",
                       Email = "tranducloc2010@gmail.com",
                       Url = new Uri("https://www.facebook.com/profile.php?id=100024381111108")
                   }
               });

               s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey,
                   Scheme = "Bearer"
               });

               s.AddSecurityRequirement(new OpenApiSecurityRequirement
           {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                       Scheme="Bearer",
                       Name="Bearer"
                    },
                    new List<string>()
                }
           });
           });
    }
}
