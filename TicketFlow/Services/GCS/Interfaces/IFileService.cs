namespace TicketFlow.Services.GCS.Interfaces;

public interface IFileService
{
    Task<string> GuardarArchivo(IFormFile contenido, string contenedor);
    Task BorrarArchivo(string ruta);
    Task<string> EditarArchivo(IFormFile contenido, string contenedor);
}