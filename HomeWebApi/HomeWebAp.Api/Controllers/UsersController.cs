using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeWebAp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse<SignUpResponse>> Post(UserRequest model)
        {
            return await service.AddUser(model);
        }

        [HttpGet]

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            return await service.GetAll();
        }

        [HttpDelete]

        public async Task<int> Delete(Guid Id)
        {
           return await service.Delete(Id);
        }

        [HttpGet("findBy")]

        public async Task<IEnumerable<UserResponse>> FindByUserName(string username)
        {
          return  await service.GetUserByName(username);
        }

        [HttpPost("ChangePassword")]

        public async Task<ApiResponse<string>> ChangePassword(ChangePasswordRequest model)
        {
            return await service.ChangePassword(model); 

        }
        [HttpPost("login")]
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest model)
        {
            return await service.Login(model);
        }
    }
}
