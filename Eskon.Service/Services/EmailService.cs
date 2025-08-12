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
        public async Task SendEmailAsync(string To, string Subject, string Body)
        {
            await _fluentEmail.To(To).Subject(Subject).Body(Body).SendAsync();
        }
    }
}
