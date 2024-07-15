using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Application.RRModels
{
    public class DepartmentRequest
    { 

        public string ? DepartmentName { get; set; }
    }
    public class DepartmentResponse:DepartmentRequest
    {
        public Guid Id { get; set; }
    }
}
