using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrainingAPi.Extesnions2
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestWebApi", Version = "v1" });

                c.AddSecurityDefinition("training_oath", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(configuration.GetSection("SwaggerConfig").GetValue<string>("TokenURL"), UriKind.Relative)
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;

        }

        public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestWebApi");
                c.OAuthAppName("Swagger");
                c.OAuthClientId("Swagger");
                c.OAuthClientSecret("Swagger");
            });
            //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestWebApi"); });
            return app;
        }

        public class AuthorizeCheckOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var hasAuthorize =
                  context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                  || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

                if (hasAuthorize)
                {
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                    operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                    operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme {Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "training_oath"}
                            }
                        ] = new[] { "TestWebApi" }
                    }
                };
                }
            }


        }
    }
}
