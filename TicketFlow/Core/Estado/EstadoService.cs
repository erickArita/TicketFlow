using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Common.Exceptions;
using TicketFlow.Core.Estado.Dtos;
using TicketFlow.DB.Contexts;

namespace TicketFlow.Core.Estado;

public class EstadoService : IEstadoService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EstadoService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //metodo para crear un estado
    public async Task<EstadoResponse> AddSync(CreateEstadoRequest createEstadoRequest)
    {
        var estadoEntity = _mapper.Map<Entities.Estado>(createEstadoRequest);

        estadoEntity.Id = Guid.NewGuid();

        _context.Estados.Add(estadoEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<EstadoResponse>(estadoEntity);
    }

    //metodo para obtener un estado por id
    public async Task<EstadoResponse> GetByIdAsync(Guid id)
    {
        var estadoEntity = await _context.Estados.FindAsync(id);

        if (estadoEntity == null)
        {
            throw new TicketFlowException("El estado no existe 😡😡");
        }

        return _mapper.Map<EstadoResponse>(estadoEntity);
    }

    //metodo para obtener todos los estados
    public async Task<IReadOnlyCollection<EstadoResponse>> GetAllAsync()
    {
        var estados = await _context.Estados.ToListAsync();
        return _mapper.Map<IReadOnlyCollection<EstadoResponse>>(estados);
    }

    //metodo para actualizar un estado
    public async Task UpdateAsync(UpdateEstadoRequest updateEstadoRequest, Guid id)
    {
        var estadoEntity = await _context.Estados.FirstOrDefaultAsync(x => x.Id == id);

        if (estadoEntity == null)
        {
            throw new TicketFlowException("El estado no existe 😡😡");
        }

        _mapper.Map(updateEstadoRequest, estadoEntity);

        await _context.SaveChangesAsync();
    }

    //metodo para eliminar un estado
    public async Task DeleteAsync(Guid id)
    {
        var estadoEntity = _context.Estados.Find(id);

        if (estadoEntity == null)
        {
            throw new TicketFlowException("El estado no existe 😡😡");
        }

        _context.Estados.Remove(estadoEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<EstadoResponse> GetByNameAsync(string name)
    {
        var estadoEntity = await _context.Estados.FirstOrDefaultAsync(x => x.Descripcion == name);

        if (estadoEntity == null)
        {
            throw new TicketFlowException("El estado no existe ❌");
        }

        return _mapper.Map<EstadoResponse>(estadoEntity);
    }
}