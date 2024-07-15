using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Persistence.Data;

namespace HomeWebApp.Persistence.Repository
{
    public  class UserRepository:BaseRepository<User>,IUserRepository
    {
        private readonly HomeWebApiDbContext context;

        public UserRepository(HomeWebApiDbContext context):base(context)
        {
            this.context = context;
        }

    }
}
