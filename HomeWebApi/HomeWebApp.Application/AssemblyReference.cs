using HomeWebApp.Application.Abstraction.IServices;
using HomeWebApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWebApp.Application
{
    public static  class AssemblyReference
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,string webRootPath)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFileService,FileService>();
            services.AddSingleton<IStorageService>(new StorageService(webRootPath));
            services.AddScoped<IDepartmentService, DepartmentService>();
           


            return services;
        }
    }
}
