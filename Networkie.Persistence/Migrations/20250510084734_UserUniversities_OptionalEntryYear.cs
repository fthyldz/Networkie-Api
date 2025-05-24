using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Networkie.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserUniversities_OptionalEntryYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUniversities",
                table: "UserUniversities");

            migrationBuilder.AlterColumn<short>(
                name: "EntryYear",
                table: "UserUniversities",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUniversities",
                table: "UserUniversities",
                columns: new[] { "UserId", "UniversityId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserUniversities_UserId_UniversityId_DepartmentId_EntryYear",
                table: "UserUniversities",
                columns: new[] { "UserId", "UniversityId", "DepartmentId", "EntryYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialPlatforms_SocialPlatformId",
                table: "UserSocialPlatforms",
                column: "SocialPlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocialPlatforms_SocialPlatforms_SocialPlatformId",
                table: "UserSocialPlatforms",
                column: "SocialPlatformId",
                principalTable: "SocialPlatforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSocialPlatforms_SocialPlatforms_SocialPlatformId",
                table: "UserSocialPlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUniversities",
                table: "UserUniversities");

            migrationBuilder.DropIndex(
                name: "IX_UserUniversities_UserId_UniversityId_DepartmentId_EntryYear",
                table: "UserUniversities");

            migrationBuilder.DropIndex(
                name: "IX_UserSocialPlatforms_SocialPlatformId",
                table: "UserSocialPlatforms");

            migrationBuilder.AlterColumn<short>(
                name: "EntryYear",
                table: "UserUniversities",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUniversities",
                table: "UserUniversities",
                columns: new[] { "UserId", "UniversityId", "DepartmentId", "EntryYear" });
        }
    }
}
