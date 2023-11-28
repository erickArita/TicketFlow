using System.Collections;

namespace TicketFlow.Common.Exceptions;

public class TicketFlowException : Exception
{
    public object Errors { get; }

    public TicketFlowException(object error) : base(error.ToString())
    {
        Errors = error;
    }
}