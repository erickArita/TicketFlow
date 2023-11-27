namespace TicketFlow.Common.Utils;

public class TicketFlowException : Exception
{
    public IEnumerable<string> Errors { get; }

    public TicketFlowException(string error)
    {
        Errors = new[] { error };
    }
}