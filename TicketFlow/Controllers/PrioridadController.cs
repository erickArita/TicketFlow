using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Prioridad;
using TicketFlow.Core.Prioridad.Dtos;

namespace TicketFlow.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PrioridadController : ControllerBase
    {
        private readonly IPrioridadService _prioridadService;

        public PrioridadController(IPrioridadService prioridadService)
        {
            _prioridadService = prioridadService;
        }
        
        [HttpGet]
        public async Task<ActionResult<AplicationResponse<IReadOnlyCollection<PrioridadResponse>>>> Get()
        {
            var prioridades = await _prioridadService.GetAllAsync();

            return Ok(
                new AplicationResponse<ICollection<PrioridadResponse>>
                {
                    Data = prioridades.ToList()
                }
            );
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AplicationResponse<PrioridadResponse>>> Get(Guid id)
        {
            var prioridad = await _prioridadService.GetByIdAsync(id);

            return Ok(
                new AplicationResponse<PrioridadResponse>
                {
                    Data = prioridad
                }
            );
        }
        
        [HttpPost]
        public async Task<ActionResult<AplicationResponse<PrioridadResponse>>> Post(CreatePrioridadRequest createPrioridad)
        {
            var prioridadResponse = await _prioridadService.AddSync(createPrioridad);

            return StatusCode(StatusCodes.Status201Created,
                new AplicationResponse<PrioridadResponse>
                {
                    Data = prioridadResponse
                }
            );
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<AplicationResponse<string>>> Put(Guid id, UpdatePrioridadRequest updatePrioridad)
        {
            await _prioridadService.UpdateAsync(updatePrioridad, id);

            return Ok(
                new AplicationResponse<string>
                {
                    Message = "Prioridad actualizada correctamente 🤩🤩"
                }
            );
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _prioridadService.DeleteAsync(id);
            
            return Ok(
                new AplicationResponse<string>
                {
                    Message = "Prioridad eliminada correctamente 🤩🤩"
                }
            );
        }
    }
}
