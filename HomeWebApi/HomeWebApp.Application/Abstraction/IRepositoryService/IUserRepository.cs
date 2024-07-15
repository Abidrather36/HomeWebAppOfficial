using HomeWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Application.Abstraction.IRepositoryService
{
    public interface IUserRepository:IBaseRepository<User>
    {
    }
}
