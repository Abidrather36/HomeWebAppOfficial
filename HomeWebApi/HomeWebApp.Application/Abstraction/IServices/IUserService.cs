using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Application.Abstraction.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<SignUpResponse>>AddUser(UserRequest model);

      
        Task<UserResponse> Update(UserUpdateRequest model);

      
        Task<int> Delete(Guid Id);


        Task<IEnumerable<UserResponse>> GetAll();


        Task<IEnumerable<UserResponse>> GetUserByName(string username);


        Task<ApiResponse<string>> ChangePassword(ChangePasswordRequest model);


        Task<ApiResponse<LoginResponse>> Login(LoginRequest model);
    }
}
