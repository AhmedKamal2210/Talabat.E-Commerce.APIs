using Microsoft.OpenApi.Models;

namespace Talabat.E_Commerce1.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDecumentation (this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TalabatApi", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization Using The Bearer Schema : => Bearer {Token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
                };

                c.AddSecurityDefinition("bearer" , securitySchema);

                var securityRequirment = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] {"bearer"} }
                };

                c.AddSecurityRequirement(securityRequirment);
            });

            return services;
        }
    }
}
