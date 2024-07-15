using HomeWebApp.Application.Abstraction.Jwt;
using HomeWebApp.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace HomeWebApp.Infrastructure.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration configuration;

        public JwtProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(AppClaimType.UserId, user.Id.ToString()),
                    new Claim(AppClaimType.Email, user.Email),
                    new Claim(AppClaimType.Contact, user.ContactNo)

                }),
                Expires = DateTime.Now.AddHours(1),
                Issuer= configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),SecurityAlgorithms.HmacSha256 ),

               
            };

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(descriptor);
            return  handler.WriteToken(securityToken);
             
           
        }
    }
}
