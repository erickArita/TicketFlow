namespace TicketFlow.Common.Utils;

public class AplicationResponse<T>
{
    public bool Status { get; set; } = true;
    public string Message { get; set; }
    public T Data { get; set; }
}

