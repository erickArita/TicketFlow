using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketFlow.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueEstadoDescripcionAndFixEstadoENTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_estados_descripcion",
                schema: "transacctional",
                table: "estados",
                column: "descripcion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_estados_descripcion",
                schema: "transacctional",
                table: "estados");
        }
    }
}
