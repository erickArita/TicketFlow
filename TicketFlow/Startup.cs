using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using TicketFlow.Common.Logger;
using TicketFlow.Core;
using TicketFlow.Core.Authentication.Utils;
using TicketFlow.DB.Contexts;
using TicketFlow.Middlewares;
using TicketFlow.Services.Email;
using TicketFlow.Services.GCS;
using TicketFlow.Services.GCS.Interfaces;
using Serilog;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace TicketFlow;

public class Startup
{
    private bool _isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        FirestoreDocumentEnricher firestoreDocumentEnricher = new(Configuration);
        // Configuración de Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .WriteTo.Console()
            .WriteTo.Sink(firestoreDocumentEnricher)
            .CreateLogger();


        // Agregar el logger como servicio
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

        services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler
            = ReferenceHandler
                .IgnoreCycles); // para solucionar el error de entra en bucle el sql porque hay una relacion de muchos a muchos


        string defaultConnection = _isProduction
            ? Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING")
            : Configuration.GetConnectionString("DefaultConnection");

        var variables = Environment.GetEnvironmentVariables();


        //Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(defaultConnection)
        );

        services.AddTransient<IEmailSenderService, MailgunEmailService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<ISigningService, SigningService>();
        services.AddAutoMapper(typeof(Startup));
        services.AddControllersWithViews();

        //Add Identity
        services.AddIdentity<IdentityUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
            .AddErrorDescriber<IdentityExceptionsEs>();

        //Add Authentication Jwt
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //para que por defecto use jwt
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true; //para que guarde el token
            options.RequireHttpsMetadata = false; //para que no use https

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, //valida el emisor
                ValidateAudience = true, //valida el receptor
                ValidateLifetime = true, //valida el tiempo de vida
                ValidateIssuerSigningKey = true, //valida la firma
                ValidIssuer = Configuration["JWT:ValidIssuer"], //el emisor debe ser el mismo que el del token
                ValidAudience = Configuration["JWT:ValidAudience"], //el receptor debe ser el mismo que el del token
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            Configuration["JWT:Secret"])), //la clave secreta debe ser la misma que la del token
                //ClockSkew = TimeSpan.Zero//para que no haya diferencia de tiempo
            };
        });

        // Add cache filter
        services.AddResponseCaching();

        //Add CORS
        services.AddCors(options =>
        {
            options.AddPolicy("CorsRule", rule => { rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"); });
        }); // para permitir que se conecte el backend con el forntend

        services.AddEndpointsApiExplorer();
        //para configurar autenticacion en swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketFlow", Version = "v1" });
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        services.ConfigureCore();
        services.AddHttpContextAccessor();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandlerMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //if (env.IsDevelopment())
        //{
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelsExpandDepth(2);
            c.DefaultModelRendering(ModelRendering.Model);
            c.DefaultModelsExpandDepth(-1);
            c.DisplayOperationId();
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.EnableFilter();
            c.ShowExtensions();
            c.ShowCommonExtensions();
            c.EnableValidator();
        });
        //}
        app.UseMiddleware<ErrorHandlerMiddlerware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseResponseCaching();

        app.UseCors("CorsRule");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}