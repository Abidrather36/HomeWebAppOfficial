using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeWebAp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService service;

        public DepartmentsController(IDepartmentService service)
        {
            this.service = service;
        }

        [HttpPost]

        public async Task<ApiResponse<DepartmentResponse>> Post(DepartmentRequest model)
        {
            return await service.Add(model);
        }

        [HttpGet]

        public async Task<ApiResponse<IEnumerable<DepartmentResponse>>> GetAll()
        {
           return  await service.GetAll();
        }


    }
}
