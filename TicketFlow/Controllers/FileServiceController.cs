using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.ArchivoAdjunto;
using TicketFlow.Core.ArchivoAdjunto.Dtos;
using TicketFlow.Services.GCS.Interfaces;

namespace TicketFlow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FileServiceController : ControllerBase
{
    private readonly IArchivoAdjuntoService _archivoAdjuntoService;

    public FileServiceController(IArchivoAdjuntoService archivoAdjuntoService)
    {
        _archivoAdjuntoService = archivoAdjuntoService;
    }

    [HttpPost]
    public async Task<ActionResult<AplicationResponse<ArchivoAdjuntoResponse>>> UploadFile(
        [FromForm] CreateArchivoAdjunto file)
    {
        var archivoAdjunto = await _archivoAdjuntoService.GuardarArchivo(file);

        return Ok(new AplicationResponse<ArchivoAdjuntoResponse>()
        {
            Message = "Archivo guardado correctamente ✅",
            Data = archivoAdjunto,
        });
    }

    [HttpPost("multiple")]
    public async Task<ActionResult<AplicationResponse<ICollection<ArchivoAdjuntoResponse>>>> UploadFiles(
        [FromForm] ICollection<CreateArchivoAdjunto> files)
    {
        var archivosAdjuntos = await _archivoAdjuntoService.GuardarArchivos(files);

        return Ok(new AplicationResponse<ICollection<ArchivoAdjuntoResponse>>()
        {
            Message = "Archivos guardados correctamente ✅",
            Data = archivosAdjuntos,
        });
    }
}