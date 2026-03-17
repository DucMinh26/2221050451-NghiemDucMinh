using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BT_NET.Migrations
{
    /// <inheritdoc />
    public partial class CreateStudentNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentNews",
                columns: table => new
                {
                    StudentCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNews", x => x.StudentCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentNews");
        }
    }
}
