namespace CityInfo.Api.Interfaces
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}