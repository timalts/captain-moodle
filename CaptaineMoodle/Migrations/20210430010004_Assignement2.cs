using Microsoft.EntityFrameworkCore.Migrations;

namespace CaptaineMoodle.Migrations
{
    public partial class Assignement2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Assignement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "6227ccd2-82ce-487f-adc2-b39fa9d61e65");

            migrationBuilder.CreateIndex(
                name: "IX_Assignement_CourseId",
                table: "Assignement",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignement_Course_CourseId",
                table: "Assignement",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignement_Course_CourseId",
                table: "Assignement");

            migrationBuilder.DropIndex(
                name: "IX_Assignement_CourseId",
                table: "Assignement");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Assignement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a3edcbcb-d8cf-4a84-8d6a-5144c9716085");
        }
    }
}
