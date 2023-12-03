namespace TicketFlow.Services.Email;

public interface IEmailSenderService
{
    Task<bool> SendEmailAsync(string emailTo, string subject, string template);
}