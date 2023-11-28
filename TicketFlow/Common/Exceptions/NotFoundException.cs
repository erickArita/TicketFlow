namespace TicketFlow.Common.Exceptions;

public class NotFoundException : Exception
{
    public object Errors { get; set; }

    public NotFoundException(object errors)
    {
        Errors = errors;
    }
}