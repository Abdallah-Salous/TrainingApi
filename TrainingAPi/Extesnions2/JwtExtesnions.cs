using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TrainingAPi.Extesnions2
{
    public static class JwtExtesnions
    {
        public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var siginingKey = Encoding.UTF8.GetBytes(configuration["JWTConfig:SecretKey"]);
                var decrKey = Encoding.UTF8.GetBytes(configuration["JWTConfig:EncryptionKey"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWTConfig:Issuer"],
                    ValidAudience = configuration["JWTConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(siginingKey),
                    TokenDecryptionKey = new SymmetricSecurityKey(decrKey)
                };
            });

            return services;
        }
    }
}
