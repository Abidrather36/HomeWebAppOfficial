using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Persistence.Data;

namespace HomeWebApp.Persistence.Repository
{
    public  class FileRepository : BaseRepository<AppFiles>, IFileRepository
    {
        public FileRepository(HomeWebApiDbContext context):base(context)
        {
                
        }
    }
}
