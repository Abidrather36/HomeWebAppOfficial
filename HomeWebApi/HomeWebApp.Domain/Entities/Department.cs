using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Domain.Entities
{
    public class Department:BaseModel
    {
        public string DepartmentName { get; set; } = null!;


        public ICollection<Employee> Employees { get; set; } = null!;
    }
}
