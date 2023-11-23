using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketFlow.DB.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityAggregateRoot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "respuesta_padre_id",
                schema: "transacctional",
                table: "respuestas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "respuestas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "respuestas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "respuestas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "respuestas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "prioridades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "prioridades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "prioridades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "prioridades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "estados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "estados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "estados",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "estados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "clientes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "clientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "archivos_tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "archivos_tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "archivos_tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "archivos_tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "archivos_respuestas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "archivos_respuestas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "archivos_respuestas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "archivos_respuestas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "archivos_adjuntos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                schema: "transacctional",
                table: "archivos_adjuntos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "archivos_adjuntos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "archivos_adjuntos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "respuestas");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "respuestas");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "respuestas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "respuestas");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "prioridades");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "prioridades");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "prioridades");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "prioridades");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "estados");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "archivos_tickets");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "archivos_tickets");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "archivos_tickets");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "archivos_tickets");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "archivos_respuestas");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "archivos_respuestas");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "archivos_respuestas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "archivos_respuestas");

            migrationBuilder.DropColumn(
                name: "ActualizadoPor",
                schema: "transacctional",
                table: "archivos_adjuntos");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                schema: "transacctional",
                table: "archivos_adjuntos");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "transacctional",
                table: "archivos_adjuntos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "transacctional",
                table: "archivos_adjuntos");

            migrationBuilder.AlterColumn<Guid>(
                name: "respuesta_padre_id",
                schema: "transacctional",
                table: "respuestas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
