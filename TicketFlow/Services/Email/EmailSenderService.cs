using SendGrid;
using SendGrid.Helpers.Mail;
using TicketFlow.Common.Exceptions;
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

    public async Task<bool> SendEmailAsync(string emailTo, string subject, string template)
    {
        emailTo = _isDevelopment ? _config.From : emailTo;

        var apikey = _config.ApiKey;
        var emailFrom = _config.From;
        var nombre = _config.Nombre;

        var cliente = new SendGridClient(apikey);
        var from = new EmailAddress(emailFrom, nombre);
        var to = new EmailAddress(emailTo, nombre);
        var contenidoHtml = template;

        var singleEmail = MailHelper.CreateSingleEmail(from, to, subject, "Hola", contenidoHtml);
        var respuesta = await cliente.SendEmailAsync(singleEmail);

        if (!respuesta.IsSuccessStatusCode)
        {
            throw new TicketFlowException("Error al enviar el email");
        }

        return true;
    }
}