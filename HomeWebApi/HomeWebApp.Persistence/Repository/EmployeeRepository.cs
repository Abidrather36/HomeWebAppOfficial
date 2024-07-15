using HomeWebApp.Application.Abstraction.IRepositoryService;
using HomeWebApp.Domain.Entities;
using HomeWebApp.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Persistence.Repository
{
    public class EmployeeRepository:BaseRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(HomeWebApiDbContext options):base(options) 
        {
                
        }
    }
}
