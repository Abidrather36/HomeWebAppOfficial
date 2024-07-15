using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWebApp.Domain.Entities
{
    public class Address:BaseModel
    {
        public string District { get; set; } = null!;


        public int Pincode { get; set; }


        public string Location { get; set; } = null!;   


        public string Landmark { get; set; } = null!;


        public Guid EmployeeId { get; set; }


        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;
    }
}
