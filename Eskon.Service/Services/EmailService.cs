using Eskon.Service.Interfaces;
using FluentEmail.Core;

namespace Eskon.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public void SendEmailAsync(string To, string Subject, string Body)
        {
            _fluentEmail.To(To).Subject(Subject).Body(Body).SendAsync();
        }

        public void SendEmailUsingRazorTemplateAsync(string To, string Subject, string Template, object Model)
        {
            _fluentEmail.To(To).Subject(Subject).UsingTemplate(Template, Model).SendAsync();
        }
    }
}
