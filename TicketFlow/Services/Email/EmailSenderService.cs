using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TicketFlow.Services.Email.Dtos;

namespace TicketFlow.Services.Email;

public class EmailSenderService : IEmailSenderService
{
    private readonly EmailConfigurationDto _config;
    private readonly bool _isDevelopment = false;

    public EmailSenderService(IConfiguration configuration)
    {
        _config = configuration.GetSection("EmailConfiguration").Get<EmailConfigurationDto>();
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            _isDevelopment = true;
        }
    }
    
    public async Task<bool> SendEmailAsync(string email, string subjet, string message)
    {
        email = _isDevelopment ? _config.FromAddress : email;
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_config.FromName, _config.FromAddress));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subjet;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = message;
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_config.SmtpServer, _config.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        } //es un using para que se desconecte del servidor

        return true;
    }
}