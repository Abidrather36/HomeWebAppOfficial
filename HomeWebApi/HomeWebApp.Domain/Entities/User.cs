using HomeWebApp.Domain.Enums;

namespace HomeWebApp.Domain.Entities
{
    public class User:BaseModel
    {


        public string UserName { get; set; } = null!;


        public string Password { get; set; } = null!;


        public string Email { get; set; }=null!;


        public string Salt { get; set; } = null!;

        public string? ResetCode { get; set; }
        public string ConfirmationCode { get; set; } = string.Empty;


        public string ContactNo { get; set; } = null!;


        public UserRole UserRole { get; set; }


        public Employee Employee { get; set; } = null!;
    }
}
