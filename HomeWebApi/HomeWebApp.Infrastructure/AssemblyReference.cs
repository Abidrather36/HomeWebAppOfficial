using HomeWebApp.Application.Abstraction.IEmailService;
using HomeWebApp.Application.Abstraction.Iidentity;
using HomeWebApp.Application.Abstraction.Jwt;
using HomeWebApp.Application.Abstraction.TemplateRenderer;
using HomeWebApp.Infrastructure.EmailService.MailJetServices;
using HomeWebApp.Infrastructure.EmailService;
using HomeWebApp.Infrastructure.Identity;
using HomeWebApp.Infrastructure.Jwt;
using HomeWebApp.Infrastructure.TemplateRenderer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWebApp.Infrastructure
{
    public static  class AssemblyReference
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MailJetOptions>(configuration.GetSection("MailJetOptionSection"));
            services.AddTransient<IEmailService, MailJetEmailService>();
            services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();

            services.AddSingleton<IJwtProvider>(new JwtProvider(configuration));
            services.AddScoped<IContextService, ContextService>();
            return services;
        }
    }
}
