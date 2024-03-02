using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api.Migrations
{
    /// <inheritdoc />
    public partial class addingimagestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefficultyId",
                table: "Walks");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDesccription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddColumn<int>(
                name: "DefficultyId",
                table: "Walks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
