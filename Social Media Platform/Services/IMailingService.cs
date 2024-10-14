namespace Social_Media_Platform.Services
{
    public interface IMailingService
    {
        Task SendEmailAsync(string mailTo,string subject, string body);
    }
}
