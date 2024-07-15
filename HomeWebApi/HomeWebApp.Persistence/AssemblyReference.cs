using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Persistence.Data;
using HomeWebApp.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWebApp.Persistence
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<HomeWebApiDbContext>(options=> options.UseSqlServer(configuration.GetConnectionString(nameof(HomeWebApiDbContext))));
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IFileRepository, FileRepository>();
            service.AddScoped<IEmployeeRepository, EmployeeRepository>();
            service.AddScoped<IDepartmentRepository, DepartmentRepository>();

            return service;
        }
    }
}
