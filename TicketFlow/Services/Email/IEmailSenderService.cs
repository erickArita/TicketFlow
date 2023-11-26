namespace TicketFlow.Services.Email;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subjet, string message);
}