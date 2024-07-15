using HomeWebApp.Domain.Entities;

namespace HomeWebApp.Application.Abstraction.Jwt
{
    public  interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}
