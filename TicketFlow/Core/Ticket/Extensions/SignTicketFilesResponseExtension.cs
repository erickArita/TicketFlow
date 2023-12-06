using Microsoft.IdentityModel.Tokens;
using TicketFlow.Core.Ticket.Dtos;
using TicketFlow.Services.GCS.Interfaces;

namespace TicketFlow.Core.Ticket.Extensions;

public static class SignTicketFilesResponseExtension
{
    public static async Task SignFiles(
        this TicketResponse archivosTickets,
        ISigningService signingService)
    {
        if (archivosTickets.ArchivosTickets.IsNullOrEmpty())
        {
            return;
        }

        var signFiles = archivosTickets.ArchivosTickets.Select(
            async archivoTicket => archivoTicket with { Link = await signingService.SignAsync(archivoTicket.Link) }
        );

        archivosTickets.ArchivosTickets = (await Task.WhenAll(signFiles)).ToList();
    }
}