using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;

namespace HomeWebApp.Application.Abstraction.IServices
{
    public interface IEmployeeService
    {
        Task<ApiResponse<EmployeeResponse>> AddEmployee(EmployeeRequest model);


        Task<EmployeeResponse> UpdateEmployee(EmployeeUpdateRequest model);


        Task<ApiResponse<EmployeeResponse>> DeleteEmployee(Guid Id);


        Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetEmployeeAll();


        Task<ApiResponse<EmployeeResponse>> GetById(Guid Id);


        Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetEmployeeByName(string Name); 
    }
}
