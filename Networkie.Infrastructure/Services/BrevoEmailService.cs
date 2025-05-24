using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using Networkie.Application.Abstractions.Services;
using Task = System.Threading.Tasks.Task;

namespace Networkie.Infrastructure.Services;

public class BrevoEmailService : IEmailService
{
    private readonly TransactionalEmailsApi _apiInstance;

    public BrevoEmailService(string apiKey)
    {
        Configuration.Default.ApiKey.Add("api-key", apiKey);
        _apiInstance = new TransactionalEmailsApi();
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        var sender = new SendSmtpEmailSender("Fatih App", "fthyldz.dev@gmail.com"); // Doğruladığın mail/dns
        var to = new List<SendSmtpEmailTo> { new(toEmail) };

        var email = new SendSmtpEmail
        {
            Sender = sender,
            To = to,
            Subject = subject,
            HtmlContent = htmlContent
        };

        await _apiInstance.SendTransacEmailAsync(email);
    }
}