using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;

namespace HomeWebApp.Application.Abstraction.IServices
{
    public interface IDepartmentService
    {
        Task<ApiResponse<DepartmentResponse>> Add(DepartmentRequest model);

        Task<ApiResponse<IEnumerable<DepartmentResponse>>> GetAll();
    }
}
