using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_user_salthash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "USER_TABs",
                newName: "Salt");

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "USER_TABs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "USER_TABs");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "USER_TABs",
                newName: "Password");
        }
    }
}
