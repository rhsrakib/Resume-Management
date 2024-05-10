using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetailsUsingJqueryAjax.Migrations
{
    /// <inheritdoc />
    public partial class next : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Experiances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Experiances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
