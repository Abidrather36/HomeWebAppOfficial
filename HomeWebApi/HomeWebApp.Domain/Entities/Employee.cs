using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Domain.Entities
{
    public class Employee:BaseModel
    {
        public string Name { get; set; } = null!;



        public string Salary { get; set; } = null!;


        public int EmpCode { get; set; }


        public bool IsActive { get; set; }


        public string? FilePath { get; set; }


        public Guid DepartmentId { get; set; }


        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;


        public ICollection<Address>? Addresses { get; set; } 

        [ForeignKey(nameof(Id))]
        public User User { get; set; } = null!;
    }
}
