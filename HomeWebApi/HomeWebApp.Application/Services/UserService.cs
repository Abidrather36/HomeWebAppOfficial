using HomeWebApp.Application.Abstraction.IEmailService;
using HomeWebApp.Application.Abstraction.Iidentity;
using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Application.Abstraction.Jwt;
using HomeWebApp.Application.Abstraction.TemplateRenderer;
using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;
using HomeWebApp.Application.Shared;
using HomeWebApp.Application.Utils;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace HomeWebApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IContextService contextService;
        private readonly IJwtProvider jwtProvider;
        private readonly IEmailService emailService;
        private readonly IEmailTemplateRenderer emailTemplateRenderer;
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly HttpContext httpContext;

        public UserService(IUserRepository repository,IContextService contextService,IJwtProvider jwtProvider,IEmailService emailService,IEmailTemplateRenderer emailTemplateRenderer,IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.contextService = contextService;
            this.jwtProvider = jwtProvider;
            this.emailService = emailService;
            this.emailTemplateRenderer = emailTemplateRenderer;
            this.httpContext = httpContextAccessor.HttpContext;
        }
        public  async Task<ApiResponse<SignUpResponse>> AddUser(UserRequest model)
        {
            if (await repository.IsExistsAsync(x => x.UserName == model.UserName))
                return ApiResponse<SignUpResponse>.ErrorResponse("UserName AlReady Exists",StatusCode.BadRequest);

            if (await repository.FirstOrDefaultAsync(x => x.Email == model.Email) is not null)

                return ApiResponse<SignUpResponse>.ErrorResponse("Email Already exists ", StatusCode.Conflict);
            User user = new User
            {
                Id=Guid.NewGuid(),
                UserName=model.UserName,
               
                Email=model.Email,
                ContactNo=model.ContactNo
                
            };
            user.Salt = AppEncryption.GenerateSalt();
            user.Password = AppEncryption.HashPassword(model.Password, user.Salt);

            user.ConfirmationCode = AppEncryption.GetRandomConfirmationCode();
            int retVal=await repository.InsertAsync(user);
            UserResponse userResponse = new UserResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ContactNo = user.ContactNo
            };
            if (retVal > 0)
            {
                var emailSetting = new MailSetting()
                {
                    To = new List<string>() { user.Email },

                    Subject = APIMessages.ConfirmEmailSubject,
                    Body = await emailTemplateRenderer.RenderTemplateAsync(APIMessages.TemplateNames.EmailConfirmation, new
                    {
                        UserName = user.UserName,
                        PhoneNumber = user.ContactNo,
                        Password = model.Password,
                        CompanyName = APIMessages.ProjectName,
                        Link = $"{HttpContextClientURL()}{AppRoutes.ClientVerifyEmailRoute}?token={user.ConfirmationCode}"
                    })
                };

                await emailService.SendEmailAsync(emailSetting);

                return ApiResponse<SignUpResponse>.SuccesResponse(new SignUpResponse
                {
                    Id=user.Id,
                    UserName=user.UserName,
                    Email=user.Email,
                    ContactNo = user.ContactNo,
                   
                } ,"User Added Successfully", StatusCode.Accepted);
            }
            else
                return null;
        }

        public async Task<ApiResponse<string>> ChangePassword(ChangePasswordRequest model)
        {
           var userId= contextService.GetUserId();
            var user=await repository.GetByIdAsync(userId);

            if (user == null)
                return ApiResponse<string>.ErrorResponse("Inavlid Credentials", StatusCode.BadRequest);

            if (!AppEncryption.ComparePassword(user.Password, model.NewPassword, user.Salt))
                return ApiResponse<string>.ErrorResponse("Invalid Old Password", StatusCode.BadRequest);

            user.Password=AppEncryption.CreatePasswordHash(model.NewPassword, user.Salt);

           int retval =await repository.UpdateAsync(user);
            if (retval > 0)
                return ApiResponse<string>.SuccesResponse(default, "Password Changed Successfully", StatusCode.Accepted);

            return ApiResponse<string>.ErrorResponse("Can't update Something went wrong  ", StatusCode.BadGateway);

        }


        public async Task<int> Delete(Guid Id)
        {
           int retVal= await repository.DeleteAsync(Id);
            if (retVal > 0)
                return 1;
            return 0;
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
           var users= await repository.GetAllAsync();
            if (users == null)
                return null;

           return  users.Select(x => new UserResponse
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                ContactNo = x.ContactNo
            });
        }

        public async  Task<IEnumerable<UserResponse>> GetUserByName(string username)
        {
          var user  =await repository.FindByAsync(x=>x.UserName == username);

           return  user.Select(x => new UserResponse
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                ContactNo = x.ContactNo
            });

            return null;
        }

        public async  Task<ApiResponse<LoginResponse>> Login(LoginRequest model)
        {
           var user= await repository.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            if (user == null)
                return ApiResponse<LoginResponse>.ErrorResponse("No User Found ", StatusCode.Accepted);

            LoginResponse logRes = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = jwtProvider.GenerateToken(user)
            };

            return ApiResponse<LoginResponse>.SuccesResponse(logRes, "User Logged in Successfully", StatusCode.Accepted);
       
        
        }

        public Task<UserResponse> Update(UserUpdateRequest model)
        {
            throw new NotImplementedException();
        }
        private string HttpContextCurrentURL()
        {
            var path = httpContext.Request.Path;
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";
        }

        private string HttpContextClientURL()
        {
            //var client = httpContext.Request.Headers["clientUrl"];
            var clientRequest = httpContext.Request.Headers["Referer"];
            return $"{clientRequest}";
        }
    }
}
