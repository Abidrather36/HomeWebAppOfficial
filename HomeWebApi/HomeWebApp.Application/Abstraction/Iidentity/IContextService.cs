namespace HomeWebApp.Application.Abstraction.Iidentity
{
    public interface IContextService
    {
        Guid GetUserId();

        string GetUserName();

        string GetEmail();


        
    }
}
