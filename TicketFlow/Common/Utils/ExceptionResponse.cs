using System.Net;

namespace TicketFlow.Common.Utils;

public class ExceptionResponse
{
    public string Title { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public object Errors { get; set; }
}