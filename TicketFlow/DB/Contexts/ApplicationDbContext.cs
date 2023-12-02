using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Entities;

namespace TicketFlow.DB.Contexts;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IHttpContextAccessor _accessor;

    public DbSet<ArchivoAdjunto> ArchivosAdjuntos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Prioridad> Prioridades { get; set; }
    public DbSet<Respuesta> Respuestas { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<ArchivoTicket> ArchivosTickets { get; set; }
    public DbSet<ArchivoRespuesta> ArchivosRespuestas { get; set; }
    public DbSet<TiketsHistory> TiketsHistory { get; set; }


    public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor accessor) : base(options)
    {
        _accessor = accessor;
    }


    protected override void OnModelCreating(ModelBuilder builder) //usar el api fluent para configurar la base de datos
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("security"); // definir un esquema para las tablas que se vana crear

        builder.Entity<IdentityUser>().ToTable("users"); // cambiar el nombre de la tabla de usuarios
        builder.Entity<IdentityRole>().ToTable("roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
        builder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");

        builder.Entity<ArchivoRespuesta>()
            .HasKey(ec => new { ec.ArchivoAdjuntoId, ec.RespuestaId });

        builder.Entity<ArchivoRespuesta>()
            .HasOne(ec => ec.ArchivoAdjunto)
            .WithMany(e => e.ArchivosRespuestas)
            .HasForeignKey(ec => ec.ArchivoAdjuntoId);

        builder.Entity<ArchivoRespuesta>()
            .HasOne(ec => ec.Respuesta)
            .WithMany(c => c.ArchivoRespuestas)
            .HasForeignKey(ec => ec.RespuestaId);

        builder.Entity<ArchivoTicket>()
            .HasKey(ec => new { ec.ArchivoAdjuntoId, ec.TicketId });

        builder.Entity<ArchivoTicket>()
            .HasOne(ec => ec.ArchivoAdjunto)
            .WithMany(e => e.ArchivosTickets)
            .HasForeignKey(ec => ec.ArchivoAdjuntoId);

        builder.Entity<ArchivoTicket>()
            .HasOne(ec => ec.Ticket)
            .WithMany(c => c.ArchivosTickets)
            .HasForeignKey(ec => ec.TicketId);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaveChangesAsync();
        return await base.SaveChangesAsync(cancellationToken);
    }


    #region Audit

    /// <summary>
    ///     con el cual hacemos reflexion para obtener los valores de las propiedades de las entidades
    ///     y asi poder guardar los cambios en la tabla de auditoria
    /// </summary>
    private void OnBeforeSaveChangesAsync()
    {
        var userName = _accessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        var utcNow = DateTime.UtcNow;

        ChangeTracker.DetectChanges();
        //iteramos sobre las entidades que implementan la interfaz IAggregateRoot que son todas excepto las tablas de Identity
        foreach (var entry in ChangeTracker.Entries<IAggregateRoot>())
        {
            if (entry.State is EntityState.Detached or EntityState.Unchanged)
                continue;


            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey()) continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreadoPor = userName;
                        entry.Entity.FechaCreacion = utcNow;
                        entry.Entity.ActualizadoPor = userName;
                        entry.Entity.FechaActualizacion = utcNow;

                        break;
                    case EntityState.Modified:
                        entry.Entity.ActualizadoPor = userName;
                        entry.Entity.FechaActualizacion = utcNow;

                        break;
                    case EntityState.Deleted:
                        //
                        break;
                }
            }
        }
    }

    #endregion
}