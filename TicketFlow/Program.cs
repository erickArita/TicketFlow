using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketFlow;
using TicketFlow.DB.Contexts;
using TicketFlow.DB.Seeders;



//-------------------------------
//conectar con firestore database------------------------------------------------
using Google.Cloud.Firestore;
string project = "db-logs-a8a8f";

FirestoreDb db = FirestoreDb.Create(project);
Console.WriteLine("Created Cloud Firestore client with project ID: {0}", project);


// insertar datos en firestore database----------------------------------------------
DocumentReference docRef = db.Collection("Ticket-Logs").Document("Tickets-4");
Dictionary<string, object> log = new Dictionary<string, object>
{
    { "fecha_accion", "3/12/2023" },
    { "accion", "creacion de ticket #260" },
    { "usuario_responsable", "Erick" },
};
await docRef.SetAsync(log);

//traer datos de firestore database-------------------------------------------
CollectionReference usersRef = db.Collection("Ticket-Logs");
QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
foreach (DocumentSnapshot document in snapshot.Documents)
{
    Console.WriteLine("Ticket: {0}", document.Id);
    Dictionary<string, object> documentDictionary = document.ToDictionary();
    Console.WriteLine("accion: {0}", documentDictionary["accion"]);
    Console.WriteLine("usuario_responsable: {0}", documentDictionary["usuario_responsable"]);
    Console.WriteLine("fecha_accion: {0}", documentDictionary["fecha_accion"]);
    Console.WriteLine();
}




//------------------------------

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

using var scope1 = app.Services.CreateScope();
var dataContext = scope1.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dataContext.Database.Migrate();

startup.Configure(app, app.Environment);

using var scope2 = app.Services.CreateScope();
var service = scope2.ServiceProvider;
var loggerFactory = service.GetRequiredService<ILoggerFactory>();

try
{
    var userManager = service.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

    await UsersRolesSeeder.LoadDataAsync(userManager, roleManager, loggerFactory);
    await PrioridadesSeeder.SeedAsync(dataContext, loggerFactory);
}
catch (Exception e)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(e, "Ocurrió un error al migrar o al insertar los datos");
}


app.Run();