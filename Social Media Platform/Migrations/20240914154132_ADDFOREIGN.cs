using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media_Platform.Migrations
{
    public partial class ADDFOREIGN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_AspNetUsers_UserId",
                table: "PostReactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_AspNetUsers_UserId",
                table: "PostReactions");
        }
    }
}
