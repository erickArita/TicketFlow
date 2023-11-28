#nullable enable
using System.Net;

namespace TicketFlow.Common.Utils;

public class AplicationResponse<T>
{
    public bool Status { get; set; } = true;
    public string Message { get; set; }
    public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.OK;
    public T? Data { get; set; }
}