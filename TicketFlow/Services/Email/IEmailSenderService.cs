﻿namespace TicketFlow.Services.Email;

public interface IEmailSenderService
{
    Task<bool> SendEmailAsync(string email, string subjet, string message);
}