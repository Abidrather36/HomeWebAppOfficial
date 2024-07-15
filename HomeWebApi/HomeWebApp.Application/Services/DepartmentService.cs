using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Application.ApiResponse;
using HomeWebApp.Application.RRModels;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            this.repository = repository;
        }
        public async  Task<ApiResponse<DepartmentResponse>> Add(DepartmentRequest model)
        {
            Department department = new Department()
            {
                Id = Guid.NewGuid(),
                DepartmentName = model.DepartmentName,
        };
           int retVal=await repository.InsertAsync(department);
            if(retVal > 0)
            {
                return ApiResponse<DepartmentResponse>.SuccesResponse(new DepartmentResponse
                {
                    Id=department.Id,
                    DepartmentName=model.DepartmentName,    
                }, "Department Inserted Successfully", StatusCode.Accepted);
            }
            return ApiResponse<DepartmentResponse>.ErrorResponse("Something went wrong", StatusCode.BadGateway);

        }

        public async Task<ApiResponse<IEnumerable<DepartmentResponse>>> GetAll()
        {
           var depts=await repository.GetAllAsync();
            if(depts is null)
            {
                return ApiResponse<IEnumerable<DepartmentResponse>>.ErrorResponse("Something is wrong please try Again",StatusCode.BadGateway);
            }

            var res=depts.Select(x => new DepartmentResponse
            {
                Id=x.Id,
                DepartmentName=x.DepartmentName,
            });
            return ApiResponse<IEnumerable<DepartmentResponse>>.SuccesResponse(res, "All Departments Info ", StatusCode.Continue);
            throw new NotImplementedException();
        }
    }
}
