using HomeWebApp.Domain.Enums;
using HomeWebApp.Domain.Enums.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Domain.Entities
{
    public class AppFiles:BaseModel
    {
        public AppModule Module { get; set; }


        public string FilePath { get; set; } = null!;


        public Guid EntityId{ get; set; }



    }
}
