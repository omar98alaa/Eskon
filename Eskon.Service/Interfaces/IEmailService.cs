namespace Eskon.Service.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string To, string Subject, string Body);
    }
}
