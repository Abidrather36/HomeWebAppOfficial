using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Persistence.Data;

namespace HomeWebApp.Persistence.Repository
{
    public class DepartmentRepository:BaseRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(HomeWebApiDbContext context):base(context)
        {
                
        }
    }
}
