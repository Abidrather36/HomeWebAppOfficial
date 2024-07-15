using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Domain.Enums;

namespace HomeWebApp.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IFileService fileService;
        private readonly IEmployeeRepository repository;
        private readonly IUserRepository userRepository;

        public EmployeeService(IFileService fileService,IEmployeeRepository repository,IUserRepository userRepository)
        {
            this.fileService = fileService;
            this.repository = repository;
            this.userRepository = userRepository;
        }
       /* public  async  Task<ApiResponse<EmployeeResponse>> AddEmployee(EmployeeRequest model)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                ContactNo=model.ContactNo,
                Password=model.Password,
                CreatedBy=Guid.NewGuid(),
                CreatedOn=DateTime.Now,

            };
            
            Employee emp = new Employee()
            {
                Id = user.Id,
                Name = model.Name,
                Salary = model.Salary,
                EmpCode = model.EmpCode,
                DepartmentId=model.DepartmentId

            };
            var appFileResponse=await fileService.UploadFileAsync(Domain.Enums.Enums.AppModule.User,emp.Id,model.File);
             int userVal= await userRepository.InsertAsync(user);

            if(userVal >0)
            {
                int val = await repository.InsertAsync(emp);
                if (appFileResponse != null && val > 0)
                {
                    return ApiResponse<EmployeeResponse>.SuccesResponse(new EmployeeResponse
                    {
                        Id = emp.Id,
                        Name = emp.Name,
                        Salary = emp.Salary,
                        DepartmentId=emp.DepartmentId,
                    }, "file inserted Successfully", StatusCode.Created);
                }
            }
           
           
            return ApiResponse<EmployeeResponse>.ErrorResponse("Wrong Access",StatusCode.Accepted);
        }*/
        public async Task<ApiResponse<EmployeeResponse>> AddEmployee(EmployeeRequest model)
        {
            // Validate the model
            if (model == null)
            {
                return ApiResponse<EmployeeResponse>.ErrorResponse("Invalid request model", StatusCode.BadRequest);
            }

            // Create a new User object
            User user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                ContactNo = model.ContactNo,
                Password = model.Password,
                CreatedBy = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
            };

            // Create a new Employee object
            Employee emp = new Employee()
            {
                Id = user.Id,
                Name = model.Name,
                Salary = model.Salary,
                EmpCode = model.EmpCode,
                DepartmentId = model.DepartmentId,
                IsActive=model.IsActive
            };

            // Upload the file and get the response
            var appFileResponse = await fileService.UploadFileAsync(Domain.Enums.Enums.AppModule.User, emp.Id, model.File);
            if (appFileResponse == null)
            {
                return ApiResponse<EmployeeResponse>.ErrorResponse("File upload failed", StatusCode.InternalServerError);
            }

            // Insert the User object into the repository
            int userVal = await userRepository.InsertAsync(user);
            if (userVal <= 0)
            {
                return ApiResponse<EmployeeResponse>.ErrorResponse("Failed to insert user", StatusCode.InternalServerError);
            }

            // Insert the Employee object into the repository
            int empVal = await repository.InsertAsync(emp);
            if (empVal > 0)
            {
                // Return a successful response
                return ApiResponse<EmployeeResponse>.SuccesResponse(new EmployeeResponse
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Salary = emp.Salary,
                    DepartmentId = emp.DepartmentId,
                    IsActive = emp.IsActive
                }, "File inserted successfully", StatusCode.Created);
            }

            // Return an error response if employee insertion failed
            return ApiResponse<EmployeeResponse>.ErrorResponse("Failed to insert employee", StatusCode.InternalServerError);
        }


        /*  public async Task<ApiResponse<EmployeeResponse>> DeleteEmployee(Guid Id)
          {
              var emp = await repository.GetByIdAsync(Id);
              EmployeeResponse empRes = new()
              {
                  Name = emp.Name,
                  Salary = emp.Salary,
                  EmpCode = emp.EmpCode,
                  DepartmentId = emp.DepartmentId,

              };
              int val= await repository.DeleteAsync(Id);

              if (val > 0)
                  return ApiResponse<EmployeeResponse>.SuccesResponse(empRes, "Employee Deleted Successfully", StatusCode.Accepted);

              return ApiResponse<EmployeeResponse>.ErrorResponse("Couldn't Delete Please Try Again ", StatusCode.BadGateway);
          }*/
        public async Task<ApiResponse<EmployeeResponse>> DeleteEmployee(Guid id)
        {
            var emp = await repository.GetByIdAsync(id);

            if (emp == null)
            {
                return ApiResponse<EmployeeResponse>.ErrorResponse("Employee not found", StatusCode.NotFound);
            }

            EmployeeResponse empRes = new()
            {
                Name = emp.Name,
                Salary = emp.Salary,
                EmpCode = emp.EmpCode,
                DepartmentId = emp.DepartmentId,
            };

            int val = await repository.DeleteAsync(emp.Id);

            if (val > 0)
            {
                return ApiResponse<EmployeeResponse>.SuccesResponse(empRes, "Employee deleted successfully", StatusCode.Accepted);
            }

            return ApiResponse<EmployeeResponse>.ErrorResponse("Couldn't delete. Please try again.", StatusCode.BadGateway);
        }

        public async Task<ApiResponse<EmployeeResponse>> GetById(Guid Id)
        {
           var emp=await repository.GetByIdAsync(Id);
            if (emp == null)
                return ApiResponse<EmployeeResponse>.ErrorResponse("No Such record ", StatusCode.BadGateway);

            EmployeeResponse empl = new()
            {
                Id = emp.Id,
                Name = emp.Name,
                Salary = emp.Salary,
                DepartmentId = emp.DepartmentId,
                

            };
            return ApiResponse<EmployeeResponse>.SuccesResponse(empl, $"Record fetched Successfully", StatusCode.Accepted);
        }

        public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetEmployeeAll()
        {
           var emps=await repository.GetAllAsync();

            if (emps is null)
                return ApiResponse<IEnumerable<EmployeeResponse>>.ErrorResponse("couldn't fetch Employees ", StatusCode.BadGateway);

            return ApiResponse<IEnumerable<EmployeeResponse>>.SuccesResponse(emps.Select( x => new EmployeeResponse
            {
                Id=x.Id,
                Name=x.Name,
                Salary=x.Salary,
                DepartmentId=x.DepartmentId,    

            }), "Employee Details", StatusCode.OK);
        }

        public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetEmployeeByName(string Name)
        {
            var user = await repository.FindByAsync(x => x.Name == Name);
            if (user is null)
                return ApiResponse<IEnumerable<EmployeeResponse>>.ErrorResponse("No Such Employee", StatusCode.BadRequest);

            return ApiResponse<IEnumerable<EmployeeResponse>>.SuccesResponse(user.Select( user => new EmployeeResponse
            {
                Id=user.Id,
                Name=user.Name,
                Salary=user.Salary,
                DepartmentId=user.DepartmentId,
                EmpCode=user.EmpCode,  
                IsActive=user.IsActive,
            }), $"{user.Count()} Employee Fetched Successfully by Name", StatusCode.Continue);
        }

        public Task<EmployeeResponse> UpdateEmployee(EmployeeUpdateRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
