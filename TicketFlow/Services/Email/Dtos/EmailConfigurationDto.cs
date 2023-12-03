namespace TicketFlow.Services.Email.Dtos;

public class EmailConfigurationDto
{
    public string ApiKey { get; set; }
    public string From { get; set; }
    public string Nombre { get; set; }
    public string Domain { get; set; }
}