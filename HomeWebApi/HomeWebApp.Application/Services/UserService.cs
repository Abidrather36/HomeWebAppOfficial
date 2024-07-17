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
using System.Net;
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
            user.Password = AppEncryption.CreatePasswordHash(model.Password, user.Salt);

            /*user.ConfirmationCode = AppEncryption.GetRandomConfirmationCode();*/
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

            if (!AppEncryption.ComparePassword(user.Password, model.OldPassowrd, user.Salt))
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

        public async  Task<ApiResponse<string>> ForgetPassword(string email)
        {
            var user = (await repository.FindByAsync(x => x.Email == email)).FirstOrDefault();
            if(user is not null)
            {
                user.ResetCode = Guid.NewGuid().ToString();
                await repository.UpdateAsync(user);
                var encodedResetCode = WebUtility.UrlEncode(user.ResetCode);

                //Link creation//
                var link = contextService.GetAppUrl() + "/users/resetPassword?resetCode=" + encodedResetCode;
                //https://localhost:7119/api/resetPassword//
                var temp = GetEmailTemplate().Replace("[LINKURL]", link);
                emailService.SendEmailAsync(new MailSetting
                {
                    To = new List<string> { email },
                    Subject = "Reset Password",
                    Body = temp,

                });
                return ApiResponse<string>.SuccesResponse(default, "password changed successfully", StatusCode.OK);
            }
            return ApiResponse<string>.ErrorResponse("Cannot change password please try again", StatusCode.BadRequest);
           

          
        }
        private string GetEmailTemplate()                             //Read File
        {
            var template = Path.Combine("EmailTemplates");
           return File.ReadAllText(Path.Combine(template, "ConfirmEmailWithUsername.html"));
        }

        public async Task<ApiResponse<string>> ResetPasword(string resetCode ,ResetPasswordRequest model)
        {
           var user = (await repository.FindByAsync(x => x.ResetCode == resetCode)).FirstOrDefault();
            if(user is not null)
            {
                user.Password = AppEncryption.CreatePasswordHash(model.NewPassword, user.Salt);
                user.ResetCode = "";
                await repository.UpdateAsync(user);
                return ApiResponse<string>.SuccesResponse(default,"Password changed Successfully", StatusCode.OK);
            }
            return ApiResponse<string>.ErrorResponse("Invalid or resetCode Expired");

        }
    }
}
