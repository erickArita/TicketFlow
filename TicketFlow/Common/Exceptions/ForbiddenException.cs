namespace TicketFlow.Common.Exceptions;

public class ForbiddenException: Exception
{
    public object Errors { get; }
    
    public ForbiddenException(object errors) : base(errors.ToString())
    {
        Errors = errors;
    }
}