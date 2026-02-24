using Microsoft.OpenApi.Models;
using Redarbor.Api.Configurations.Definitions;

namespace Redarbor.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services
            .AddSwaggerGen(options =>
            {
                options
                    .AddSecurityDefinition("TokenAuth", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Token-based authentication and authorization", 
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header
                    });

                options
                    .AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "TokenAuth"
                                }
                            },
                            new List<string>()
                        }
                    });

                var xmlFileName = $"{AssemblyReference.Assembly.GetName().Name}.xml";

                options
                    .SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "RedArbor Employee Technical Test",
                        Version = "v1",
                        Description = "API for employee management"
                    });

                options
                    .DocumentFilter<AutoTagDescriptionsDocumentFilter>();
            });
        return services;
    }
}
