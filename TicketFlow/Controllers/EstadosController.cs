using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Estado;
using TicketFlow.Core.Estado.Dtos;

namespace TicketFlow.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadoService _estadoService;

        public EstadosController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<EstadoResponse>>> Get()
        {
            var estados = await _estadoService.GetAllAsync();
            return Ok(
                new AplicationResponse<ICollection<EstadoResponse>>
                {
                    Data = estados.ToList()
                }
            );
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AplicationResponse<EstadoResponse>>> GetOneById(Guid id)
        {
            var estado = await _estadoService.GetByIdAsync(id);
            
            return Ok(
                new AplicationResponse<EstadoResponse>
                {
                    Data = estado
                }
            );
        }
        
        [HttpPost]
        public async Task<ActionResult<AplicationResponse<EstadoResponse>>> Post(CreateEstadoRequest createEstadoRequest)
        {
            var estado = await _estadoService.AddSync(createEstadoRequest);
            
            return Ok(
                new AplicationResponse<EstadoResponse>
                {
                    Data = estado
                }
            );
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<AplicationResponse<string>>> Put(UpdateEstadoRequest updateEstadoRequest, Guid id)
        {
            await _estadoService.UpdateAsync(updateEstadoRequest, id);
            
            return Ok(
                new AplicationResponse<string>
                {
                    Message = "Estado actualizado con exito 😎😎"
                }
            );
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _estadoService.DeleteAsync(id);
            
            return Ok(
                new AplicationResponse<string>
                {
                    Message = "Estado eliminado con exito 😎😎"
                }
            );
        }
    }
}