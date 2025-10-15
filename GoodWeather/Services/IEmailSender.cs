namespace GoodWeather.Services
{
    public interface IEmailSender
    {
        void Send(string subject, string body);
    }
}