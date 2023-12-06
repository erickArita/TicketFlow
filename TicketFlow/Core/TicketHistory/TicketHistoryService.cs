using TicketFlow.Core.User;
using TicketFlow.DB.Contexts;
using TicketFlow.Entities;

namespace TicketFlow.Core.TicketHistory;

public class TicketHistoryService : ITicketHistoryService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserService _userService;

    public TicketHistoryService(ApplicationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }
    
    public async Task AddTicketHistoryAsync(string descripcion, Guid ticketId)
    {
        var user = await _userService.GetUserInSessionAsync();
        var ticketHistory = new TiketsHistory
        {
            Id = Guid.NewGuid(),
            Descripcion = descripcion,
            UsuarioId = user.Id,
            TicketId = ticketId
        };

        _dbContext.TiketsHistory.Add(ticketHistory);
        await _dbContext.SaveChangesAsync();
    }
}