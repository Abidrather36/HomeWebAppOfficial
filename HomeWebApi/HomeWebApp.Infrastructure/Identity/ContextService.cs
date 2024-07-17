using HomeWebApp.Application.Abstraction.Iidentity;
using HomeWebApp.Infrastructure.Jwt;
using Microsoft.AspNetCore.Http;

namespace HomeWebApp.Infrastructure.Identity
{
    public class ContextService : IContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetEmail()
        {
            var email=  httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == AppClaimType.UserId)?.Value;
            if (email is null) return string.Empty;
            return email;
        }

        public Guid GetUserId()
        {
           var id= httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == AppClaimType.UserId)?.Value;
            if(id is null) return Guid.Empty;

            Guid Id = Guid.Parse(id);
            return Id;
        }

        public string GetUserName()
        {
            var usrName = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == AppClaimType.UserName)?.Value;
            if(usrName is null) return string.Empty;

            return usrName; 
        }
        public string GetAppUrl()
        {
            var request=httpContextAccessor.HttpContext?.Request;
            return request?.Scheme + "//" + request?.Host+"/" + "api/";
        }
    }
}
