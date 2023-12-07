using TicketFlow.Core.ArchivoAdjunto.Dtos;

namespace TicketFlow.Core.ArchivoAdjunto;

public interface IArchivoAdjuntoService
{
    Task<ArchivoAdjuntoResponse> GuardarArchivo(CreateArchivoAdjunto files);
    Task<ICollection<ArchivoAdjuntoResponse>> GuardarArchivos(ICollection<IFormFile> files);
}