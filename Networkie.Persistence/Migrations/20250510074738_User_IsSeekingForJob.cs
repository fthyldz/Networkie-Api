using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Networkie.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class User_IsSeekingForJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeekingForJob",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserUniversities_DepartmentId",
                table: "UserUniversities",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUniversities_UniversityId",
                table: "UserUniversities",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUniversities_Departments_DepartmentId",
                table: "UserUniversities",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUniversities_Universities_UniversityId",
                table: "UserUniversities",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUniversities_Departments_DepartmentId",
                table: "UserUniversities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUniversities_Universities_UniversityId",
                table: "UserUniversities");

            migrationBuilder.DropIndex(
                name: "IX_UserUniversities_DepartmentId",
                table: "UserUniversities");

            migrationBuilder.DropIndex(
                name: "IX_UserUniversities_UniversityId",
                table: "UserUniversities");

            migrationBuilder.DropColumn(
                name: "IsSeekingForJob",
                table: "Users");
        }
    }
}
