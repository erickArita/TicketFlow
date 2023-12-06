using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketFlow.DB.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoNombreCampoArchivoAdjunto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "link",
                schema: "transacctional",
                table: "archivos_adjuntos",
                newName: "object_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "object_id",
                schema: "transacctional",
                table: "archivos_adjuntos",
                newName: "link");
        }
    }
}
