namespace TicketFlow.Services.Email.Dtos;

public record EmailSenderConfigurarionSmtp(
    string SmtServer,
    int SmtPort,
    string SmtpPassword
);