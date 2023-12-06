using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketFlow.DB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "transacctional");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "archivos_adjuntos",
                schema: "transacctional",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivos_adjuntos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                schema: "transacctional",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                schema: "transacctional",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prioridades",
                schema: "transacctional",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prioridades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles_claims",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roles_claims_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                schema: "transacctional",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    cliente_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    prioridad_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usuario_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_tickets_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalSchema: "transacctional",
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_estados_estado_id",
                        column: x => x.estado_id,
                        principalSchema: "transacctional",
                        principalTable: "estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_prioridades_prioridad_id",
                        column: x => x.prioridad_id,
                        principalSchema: "transacctional",
                        principalTable: "prioridades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_users_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_claims",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_claims_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_logins",
                schema: "security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_users_logins_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_roles",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_users_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_roles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_tokens",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_users_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "archivos_tickets",
                schema: "transacctional",
                columns: table => new
                {
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    archivo_adjunto_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivos_tickets", x => new { x.archivo_adjunto_id, x.ticket_id });
                    table.ForeignKey(
                        name: "FK_archivos_tickets_archivos_adjuntos_archivo_adjunto_id",
                        column: x => x.archivo_adjunto_id,
                        principalSchema: "transacctional",
                        principalTable: "archivos_adjuntos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_archivos_tickets_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalSchema: "transacctional",
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "respuestas",
                schema: "transacctional",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usuario_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    respuesta_padre_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas", x => x.id);
                    table.ForeignKey(
                        name: "FK_respuestas_respuestas_respuesta_padre_id",
                        column: x => x.respuesta_padre_id,
                        principalSchema: "transacctional",
                        principalTable: "respuestas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_respuestas_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalSchema: "transacctional",
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_respuestas_users_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tickets_history",
                schema: "transacctional",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usuario_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_tickets_history_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalSchema: "transacctional",
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_history_users_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",             
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "archivos_respuestas",
                schema: "transacctional",
                columns: table => new
                {
                    respuesta_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    archivo_adjunto_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivos_respuestas", x => new { x.archivo_adjunto_id, x.respuesta_id });
                    table.ForeignKey(
                        name: "FK_archivos_respuestas_archivos_adjuntos_archivo_adjunto_id",
                        column: x => x.archivo_adjunto_id,
                        principalSchema: "transacctional",
                        principalTable: "archivos_adjuntos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_archivos_respuestas_respuestas_respuesta_id",
                        column: x => x.respuesta_id,
                        principalSchema: "transacctional",
                        principalTable: "respuestas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivos_respuestas_respuesta_id",
                schema: "transacctional",
                table: "archivos_respuestas",
                column: "respuesta_id");

            migrationBuilder.CreateIndex(
                name: "IX_archivos_tickets_ticket_id",
                schema: "transacctional",
                table: "archivos_tickets",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_respuesta_padre_id",
                schema: "transacctional",
                table: "respuestas",
                column: "respuesta_padre_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_ticket_id",
                schema: "transacctional",
                table: "respuestas",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_usuario_id",
                schema: "transacctional",
                table: "respuestas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "security",
                table: "roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_roles_claims_RoleId",
                schema: "security",
                table: "roles_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_cliente_id",
                schema: "transacctional",
                table: "tickets",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_estado_id",
                schema: "transacctional",
                table: "tickets",
                column: "estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_prioridad_id",
                schema: "transacctional",
                table: "tickets",
                column: "prioridad_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_usuario_id",
                schema: "transacctional",
                table: "tickets",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_history_ticket_id",
                schema: "transacctional",
                table: "tickets_history",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_history_usuario_id",
                schema: "transacctional",
                table: "tickets_history",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "security",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "security",
                table: "users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_claims_UserId",
                schema: "security",
                table: "users_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_logins_UserId",
                schema: "security",
                table: "users_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_roles_RoleId",
                schema: "security",
                table: "users_roles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivos_respuestas",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "archivos_tickets",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "roles_claims",
                schema: "security");

            migrationBuilder.DropTable(
                name: "tickets_history",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "users_claims",
                schema: "security");

            migrationBuilder.DropTable(
                name: "users_logins",
                schema: "security");

            migrationBuilder.DropTable(
                name: "users_roles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "users_tokens",
                schema: "security");

            migrationBuilder.DropTable(
                name: "respuestas",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "archivos_adjuntos",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "tickets",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "clientes",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "estados",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "prioridades",
                schema: "transacctional");

            migrationBuilder.DropTable(
                name: "users",
                schema: "security");
        }
    }
}
