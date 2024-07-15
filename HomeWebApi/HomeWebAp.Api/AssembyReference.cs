using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HomeWebAp.Api
{
    public static class  AssembyReference
    {
        public static  IServiceCollection AddApiService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))

                };
            });
            return services;
        }
    }
}
