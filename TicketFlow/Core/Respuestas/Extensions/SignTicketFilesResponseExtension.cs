using Microsoft.IdentityModel.Tokens;
using TicketFlow.Core.Ticket.Dtos;
using TicketFlow.Services.GCS.Interfaces;

namespace TicketFlow.Core.Respuestas.Extensions;

public static class SignRespuestaFilesExtension
{
    public static async Task SignFiles(
        this RespuestaResponse archivosResponse,
        ISigningService signingService)
    {
        if (archivosResponse.ArchivoRespuestas.IsNullOrEmpty())
        {
            return;
        }

        var signFiles = archivosResponse.ArchivoRespuestas.Select(
            async archivoTicket => archivoTicket with { Link = await signingService.SignAsync(archivoTicket.Link) }
        );

        archivosResponse.ArchivoRespuestas = (await Task.WhenAll(signFiles)).ToList();
    }
}