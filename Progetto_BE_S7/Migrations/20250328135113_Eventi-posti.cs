using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetto_BE_S7.Migrations
{
    /// <inheritdoc />
    public partial class Eventiposti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numeroBiglietti",
                table: "Eventi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numeroBiglietti",
                table: "Eventi");
        }
    }
}
