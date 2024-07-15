using HomeWebApp.Domain.Enums.Enums;
using Practyc.Domain.Enums;

namespace Practyc.Application.RRModel
{
    public class AppFileResponse
    {
        public Guid  Id { get; set; }


        public string FilePath { get; set; } = string.Empty;


        public Guid EntityId { get; set; }


        public AppModule AppModule { get; set; }
    }

}
