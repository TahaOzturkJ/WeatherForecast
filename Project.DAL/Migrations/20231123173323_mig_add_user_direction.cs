using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_user_direction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Log",
                table: "USER_LOG_TABs");

            migrationBuilder.AddColumn<int>(
                name: "Yon",
                table: "USER_LOG_TABs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Yon",
                table: "USER_LOG_TABs");

            migrationBuilder.AddColumn<string>(
                name: "Log",
                table: "USER_LOG_TABs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
