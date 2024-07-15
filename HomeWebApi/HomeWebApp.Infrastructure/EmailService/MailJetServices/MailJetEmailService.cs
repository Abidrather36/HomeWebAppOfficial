using HomeWebApp.Application.Abstraction.IEmailService;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Infrastructure.EmailService.MailJetServices
{
    internal class MailJetEmailService : IEmailService
    {
        private readonly IOptions<MailJetOptions> options;

        public MailJetEmailService(IOptions<MailJetOptions> options)
        {
            this.options = options;
        }
        public async Task<bool> SendEmailAsync(MailSetting setting)
        {
            MailjetClient client = new(options?.Value.ApiKey, options?.Value.SecretKey);

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(options?.Value.FromEmail))
                .WithSubject(setting.Subject)
                .WithHtmlPart(setting.Body)
                .WithTo(new SendContact(setting.To.FirstOrDefault()))
                .Build();



            var response = await client.SendTransactionalEmailAsync(email);
            return
                response?.Messages?.FirstOrDefault()?.Status is not null
                ?
                response?.Messages?.FirstOrDefault()?.Status == "success"
                :
                false;
        }
    }
}
