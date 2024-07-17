using Practyc.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Application.RRModels
{
    public class UserRequest
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Please Enter Valid Email"), MinLength(6)]

        public string Email { get; set; } = null!;


        public string ContactNo { get; set; } = null!;


    }

    public class UserUpdateRequest
    {
        public Guid Id { get; set; }


        public string Email { get; set; } = null!;


        public string ContactNo { get; set; } = null!;
    }

    public class UserResponse
    {

        public Guid Id { get; set; }


        public string UserName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string ContactNo { get; set; } = null!;



    }
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Enter Old Password")]

        public string OldPassowrd { get; set; } = string.Empty;


        [Required(ErrorMessage = "Enter New password")]
        public string NewPassword { get; set; } = string.Empty;


        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = string.Empty;


    }

    public class LoginRequest
    {
        public string? UserName { get; set; }


        public string? Password { get; set; }
    }

    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }

        public string? Token { get; set; }

    }

    public class SignUpResponse
    {
        public Guid Id { get; set; }


        public string? UserName { get; set; }


        public string Email { get; set; } = string.Empty;


        public string ContactNo { get; set; } = string.Empty;


    }

    public class ResetPasswordRequest
    {
        public string? NewPassword { get; set; }

        public string? ConfirmPassword { get; set; }

        /*public string ResetCode { get; set; }*/
    }
}
