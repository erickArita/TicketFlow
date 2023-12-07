using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.Prioridad.Dtos;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities;

namespace TicketFlow.Core.Prioridad;

public class PrioridadService : IPrioridadService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PrioridadService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    //metodo para crear una prioridad
    public async Task<PrioridadResponse> AddSync(CreatePrioridadRequest createPrioridadRequest)
    {
        var prioridadEntity = _mapper.Map<Entities.Prioridad>(createPrioridadRequest);

        prioridadEntity.Id = Guid.NewGuid();

        _context.Prioridades.Add(prioridadEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<PrioridadResponse>(prioridadEntity);
    }
    
    //metodo para obtener una prioridad por id
    public async Task<PrioridadResponse> GetByIdAsync(Guid id)
    {
        var prioridadEntity = await _context.Prioridades.FindAsync(id);

        if (prioridadEntity == null)
        {
            throw new TicketFlowException("La prioridad no existe 😡😡");
        }

        return _mapper.Map<PrioridadResponse>(prioridadEntity);
    }
    
    //metodo para obtener todas las prioridades
    public async Task<IReadOnlyCollection<PrioridadResponse>> GetAllAsync()
    {
        var prioridades = await _context.Prioridades.ToListAsync();
        return _mapper.Map<IReadOnlyCollection<PrioridadResponse>>(prioridades);
    }
    
    //metodo para actualizar una prioridad
    public async Task<PrioridadResponse> UpdateAsync(UpdatePrioridadRequest updatePrioridadRequest, Guid id)
    {
        var prioridadEntity = await _context.Prioridades.FirstOrDefaultAsync(x => x.Id == id);

        if (prioridadEntity == null)
        {
            throw new TicketFlowException("La prioridad no existe 😡😡");
        }

        _mapper.Map(updatePrioridadRequest, prioridadEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<PrioridadResponse>(prioridadEntity);
    }
    
    //metodo para eliminar una prioridad
    public async Task DeleteAsync(Guid id)
    {
        var prioridadEntity = _context.Prioridades.Find(id);

        if (prioridadEntity == null)
        {
            throw new TicketFlowException("La prioridad no existe 😡😡");
        }

        _context.Prioridades.Remove(prioridadEntity);
        await _context.SaveChangesAsync();
    }
}