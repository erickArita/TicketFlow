namespace TicketFlow.Services.GCS.Interfaces;

public interface ISigningService
{
    Task<string> SignAsync(string imageUrl, TimeSpan expiration);
    Task<string> SignAsync(string imageUrl);
}