using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBanUserAndLessonDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IaBanned",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IaBanned",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lessons");
        }
    }
}
