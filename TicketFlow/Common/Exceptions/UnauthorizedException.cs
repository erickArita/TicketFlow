namespace TicketFlow.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public object Errors { get; }

    public UnauthorizedException(object errors) : base(errors.ToString())
    {
        Errors = errors;
    }
}