using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearningSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCoinSystemAndLessonTracker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the check constraint
            migrationBuilder.DropCheckConstraint(
                name: "CK_Course_Price",
                table: "Courses");

            // Alter column
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            // Recreate the check constraint
            migrationBuilder.AddCheckConstraint(
                name: "CK_Course_Price",
                table: "Courses",
                sql: "[Price] >= 0");


            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Payments_PaymentId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_PaymentId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "Coins",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 100);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastViewedLesson",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentId",
                table: "Payments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_StudentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Coins",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LastViewedLesson",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Courses",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_PaymentId",
                table: "Enrollments",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Payments_PaymentId",
                table: "Enrollments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
