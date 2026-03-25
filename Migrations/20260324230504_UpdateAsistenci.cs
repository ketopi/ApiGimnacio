using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Gimnacio.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAsistenci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_Clientes_ClienteId",
                table: "Asistencias");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Asistencias",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_Clientes_ClienteId",
                table: "Asistencias",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_Clientes_ClienteId",
                table: "Asistencias");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Asistencias",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_Clientes_ClienteId",
                table: "Asistencias",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
