namespace TicketFlow.Services.GCS.Dtos;

public record GCPConfig
{
    public string ProjectId  { get; set; }
    public string BucketName  { get; set; }
};