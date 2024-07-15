using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWebApp.Application.Abstraction.IEmailService
{
    public  interface IEmailService
    {
        Task<bool> SendEmailAsync(MailSetting setting);
    }
}
