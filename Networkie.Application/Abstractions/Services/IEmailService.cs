namespace Networkie.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string htmlContent);
}