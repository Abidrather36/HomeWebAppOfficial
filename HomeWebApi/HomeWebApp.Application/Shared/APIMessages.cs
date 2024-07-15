using static HomeWebApp.Application.Shared.APIMessages;

namespace HomeWebApp.Application.Shared;

public static class APIMessages
{
    // Common Errors 
    public static readonly string TechnicalError = "There is some technical error, please try again later.";

    public static readonly string ProjectName = "Home Web-Api ";

    public static readonly string NotFound = "Not found.";

    public static readonly string UpdatedSuccessfully = "Updated Successfully.";

    public static readonly string VerifyEmailLink = "api/auth/verifyemail";

    public static readonly string ValidationException = "Validation error";

    public static readonly string ForbiddenException = "Forbidden error";

    public static readonly string InfoOrAndConflictException = "Info/Conflict error";

    public static readonly string DbUpdateException = "Database Update Exception";

    public static readonly string ResetPasswordLink = "api/auth/resetpassword";

    public static readonly string Success = "Success.";

    public static readonly string Error = "Error.";

    public static readonly string InvaildStatusCode = "Invalid status code.";

    public static readonly string Admin = "Admin";

    public static readonly string EmailTemplates = "EmailTemplates";

    public static readonly string Templates = "D:\\Repository\\KashmirService\\API\\KashmirServices-Api\\KashmirService.Infrastructure\\EmailTemplates";

    public static readonly string ConfirmEmailSubject = "Verify Your Email And Complete Registration";

    public static readonly string PasswordResetEmailSubject = "Reset Your Password";

    public static class Addresses
    {
        public static readonly string NoAddressFound = "No addresses found";

        public static readonly string AddressUpdated = "Address updated successfully";
    }

    public static class ServiceManagement
    {
        public static readonly string ServiceNotFound = "Service not found.";

        public static readonly string ServiceAdded = "Service Added Successfully.";

        public static readonly string ServiceUpdated = "Service Updated Successfully.";
    }

    public static class Auth
    {
        public const string UsernameAlreadyTaken = "Username is already taken.";
       

        public const string EmailAlreadyRegistered = "Email already registered.";

        public const string PhoneNumberAlreadyRegistered = "Contact Number already registered.";

        public const string UsernameOrPasswordIsIncorrect = "Username or/and Password is Incorrect.";

        public const string PasswordChangedSuccess = "Password changed successfully.";

        public const string LinkExpired = "Email verification link has been expired, please try again.";

        public const string EmailVerified = "Email verified successfully, please try to login again.";

        public const string VerifyEmailError = "Please verify your email to login.";

        public const string InactiveUser = "Your account is temporarily inactive. Please contact the administrator for assistance.";

        public const string InVaildEmailAddress = "User not found, please enter a vaild email.";

        public const string CheckEmailToResetPassword = "Please check your email inbox for instructions on how to reset your password.";

        public const string PasswordResetSuccess = "Your password has been successfully reset. You can now log in using your new password.";
    }

    public static class TemplateNames
    {
        public static readonly string EmailConfirmation = "EmailConfirmation.cshtml";

        public static readonly string PasswordReset = "PasswordReset.cshtml";

        public static readonly string ConfirmEmailWithUsername = "ConfirmEmailWithUsername.cshtml";
    }


}

public static class AppRoutes
{
    public static readonly string ClientVerifyEmailRoute = "verifyemail";
    public static readonly string ClientResetPasswordRoute = "resetpassword";
}