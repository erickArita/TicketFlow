using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TicketFlow.Services.Email.Dtos;


namespace TicketFlow.Services.Email;

public class EmailSenderSmtp : IEmailSenderService
{
    private readonly EmailConfigurationDto _config;
    private readonly EmailSenderConfigurarionSmtp _configurarionSmtp;

    public EmailSenderSmtp(
        IConfiguration configuration)
    {
        _configurarionSmtp = configuration.GetSection("EmailConfigurationSmtp").Get<EmailSenderConfigurarionSmtp>();
        _config = configuration.GetSection("EmailConfiguration").Get<EmailConfigurationDto>();
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_config.From, _config.From));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = message,
        };

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_configurarionSmtp.SmtServer,
            _configurarionSmtp.SmtPort, SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(_config.From, _configurarionSmtp.SmtpPassword);

        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
        return true;
    }
}