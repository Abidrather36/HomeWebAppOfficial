using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeWebAp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService service;

        public EmployeesController(IEmployeeService service)
        {
            this.service = service;
        }

        [HttpPost]

        public async  Task<ApiResponse<EmployeeResponse>> PostFile([FromForm] EmployeeRequest model)
        {
           return await service.AddEmployee(model);
        }
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetAll()
        {
          return  await service.GetEmployeeAll();
        }
        [HttpDelete]
        public async Task<ApiResponse<EmployeeResponse>> Delete(Guid Id)
        {
          return  await service.DeleteEmployee(Id);
        }

        [HttpGet("Id")]
        public async Task<ApiResponse<EmployeeResponse>> GetbyId(Guid Id)
        {
          return  await service.GetById(Id);
        }


        [HttpGet("FindByName")]

        public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetByName(string Name)
        {
            return await service.GetEmployeeByName(Name);
        }
    }
}
