using HomeWebApp.Domain.Interface;

namespace HomeWebApp.Domain.Entities
{
    public class BaseModel : IBaseModel
    {
        public Guid Id { get ; set ; }
        
        
        public DateTimeOffset CreatedOn { get ; set ; }

        
        public DateTimeOffset UpdatedOn { get; set; }
        
        public Guid UpdatedBy { get; set; }
        
        
        public Guid CreatedBy { get; set; }
    }
}
