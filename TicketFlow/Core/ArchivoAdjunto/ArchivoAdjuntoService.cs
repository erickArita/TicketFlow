using TicketFlow.Core.ArchivoAdjunto.Dtos;
using TicketFlow.DB.Contexts;
using TicketFlow.Services.GCS.Interfaces;

namespace TicketFlow.Core.ArchivoAdjunto;

public class ArchivoAdjuntoService : IArchivoAdjuntoService
{
    private readonly IFileService _fileService;
    private readonly ISigningService _signingService;
    private readonly ApplicationDbContext _context;

    public ArchivoAdjuntoService(IFileService fileService, ISigningService signingService,ApplicationDbContext context)
    {
        _fileService = fileService;
        _signingService = signingService;
        _context = context;
    }

    public async Task<ArchivoAdjuntoResponse> GuardarArchivo(CreateArchivoAdjunto contenido)
    {
        var objectName = await _fileService.GuardarArchivo(contenido.Archivo, "archivos");
        var archivoAdjunto = new Entities.ArchivoAdjunto()
        {
            Id = Guid.NewGuid(),
            ObjectId = objectName
        };
            
        await _context.ArchivosAdjuntos.AddAsync(archivoAdjunto);
        await _context.SaveChangesAsync();
      

        var link = await _signingService.SignAsync(objectName);
    
        return new ArchivoAdjuntoResponse(archivoAdjunto.Id, link);
    }

    public async Task<ICollection<ArchivoAdjuntoResponse>> GuardarArchivos(ICollection<CreateArchivoAdjunto> files)
    {
        var archivosAdjuntos = new List<ArchivoAdjuntoResponse>();
        foreach (var file in files)
        {
            var archivoAdjunto = await GuardarArchivo(file);
            archivosAdjuntos.Add(archivoAdjunto);
        }

        return archivosAdjuntos;
    }
}