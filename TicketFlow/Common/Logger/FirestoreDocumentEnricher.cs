using System.Text.Json;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Serilog.Core;
using Serilog.Events;
using TicketFlow.Services.GCS.Dtos;


namespace TicketFlow.Common.Logger;

public class FirestoreDocumentEnricher : ILogEventSink
{
    private readonly string _coleccion = "Logs";
    private readonly FirestoreDb _firestoreDb;

    public FirestoreDocumentEnricher(IConfiguration configuration)
    {
        var gcpConfig = configuration.GetSection("GCPConfig").Get<GCPConfig>() ??
                        throw new ArgumentNullException(nameof(configuration));
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Services/GCS/Credentials/ClientCredentials.json");

        var client = new FirestoreClientBuilder
        {
            CredentialsPath = path
        }.Build();
        _firestoreDb = FirestoreDb.Create(gcpConfig.ProjectId, client);
    }


    private async Task AgregarLogFirestore(AllData logData)
    {
        var logCollection = _firestoreDb.Collection(_coleccion);
        var logDocument = logCollection.Document();

        var dictionary = new Dictionary<string, object>
        {
            { "OperationType", logData.OperationType },
            { "Before", logData.Before },
            { "After", logData.After },
            { "Timestamp", logData.Timestamp },
            { "Level", logData.Level },
            { "Message", logData.Message },
            { "exception", logData.exception }
        };

        await logDocument.SetAsync(dictionary);
    }

    public void Write(LogEvent logEvent)
    {
        return;
    }

    public void Emit(LogEvent logEvent)
    {
        var level = logEvent.Level;
        var logMessage = logEvent.RenderMessage();
        var exception = logEvent.Exception;
        var state = logEvent.Properties;
        var logState = logEvent.Properties.TryGetValue("logState", out var test) ? test : null;

        var isJsonString = logMessage.StartsWith("{") && logMessage.EndsWith("}");
        AllData allData = new();
        if (isJsonString)
        {
            var logDataParced = (LogState)JsonSerializer.Deserialize<LogState>(logMessage);
            var stringBefore = logDataParced.Before?.ToString();
            var stringAfter = logDataParced.After?.ToString();

            var bewforeIfUpdate = stringBefore == stringAfter ? "" : stringBefore;
            allData = new AllData
            {
                OperationType = logDataParced.OperationType,
                Before = bewforeIfUpdate,
                After = stringAfter,
                Timestamp = DateTime.UtcNow.ToString(),
                Level = level.ToString(),
                Message = logMessage,
                exception = exception?.ToString()
            };
        }
        else
        {
            allData = new AllData
            {
                OperationType = OperationTypes.Log,
                Before = "",
                After = "",
                Timestamp = DateTime.UtcNow.ToString(),
                Level = level.ToString(),
                Message = logMessage,
                exception = exception?.ToString(),
                Title = logMessage.Contains("---") ? logMessage : ""
            };
        }

        AgregarLogFirestore(allData).Wait();
    }
}

public class LogState
{
    public string OperationType { get; set; }
    public object Before { get; set; }
    public object After { get; set; }
    public string exception { get; set; }
    public string? Title { get; set; }
}

public class AllData : LogState
{
    public string Level { get; set; }
    public string Message { get; set; }
    public string Timestamp { get; set; }
}