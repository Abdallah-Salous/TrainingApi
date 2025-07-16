using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public static JwtSecurityToken GenerateToken(this ClaimsPrincipal user, IConfiguration configuration)
        {
            var claimsIdentity = new ClaimsIdentity(user.Identity);
            var jwtConfig = configuration.GetSection("JWTConfig");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.GetValue<string>("SecretKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var encKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.GetValue<string>("EncryptionKey")));
            var encCred = new EncryptingCredentials(encKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var jwh = new JwtSecurityTokenHandler();
            var token = jwh.CreateJwtSecurityToken(
                                jwtConfig.GetValue<string>("Issuer"),
                                jwtConfig.GetValue<string>("Audience"),
                                claimsIdentity,
                                null,
                                DateTime.Now.AddMinutes(jwtConfig.GetValue<double>("ValidMins")),
                                null,
                                creds,
                                encCred);

            return token;
        }
    }
}
