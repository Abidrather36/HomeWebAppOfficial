using Microsoft.AspNetCore.Http;

namespace HomeWebApp.Application.RRModels
{
    public class EmployeeRequest
    {

        
        public string Name { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public String Email { get; set; } = null!;


        public string ContactNo { get; set; } = null!;


        public string Salary { get; set; } = null!;


        public string Password { get; set; } = null!;


        public int EmpCode { get; set; }


        public bool IsActive { get; set; }


        public IFormFile File { get; set; } = null!;


        public Guid DepartmentId { get; set; }
    }

    public class EmployeeResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;


        public string Salary { get; set; } = null!;


        public int EmpCode { get; set; }


        public bool IsActive { get; set; }


        public string FilePath { get; set; } = null!;


        public Guid DepartmentId { get; set; }
    }

    public class EmployeeUpdateRequest
    {
        public string Name { get; set; } = null!;

    }
}
