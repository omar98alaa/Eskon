namespace Eskon.Service.Interfaces
{
    public interface IEmailService
    {
        public void SendEmailAsync(string To, string Subject, string Body);

        public void SendEmailUsingRazorTemplateAsync(string To, string Subject, string Template, object Model);
    }
}
