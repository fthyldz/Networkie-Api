using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Networkie.Application.Abstractions.Services;

namespace Networkie.Infrastructure.Services;

public class GmailEmailService(IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string bodyHtml)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(configuration.GetValue<string>("Gmail:Email")));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = bodyHtml };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(configuration.GetValue<string>("Gmail:Email"), configuration.GetValue<string>("Gmail:AppPassword"));
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}