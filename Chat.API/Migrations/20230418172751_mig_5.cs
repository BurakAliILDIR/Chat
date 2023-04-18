using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.API.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Meets_MeetId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MeetId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MeetId1",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Meets",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MeetId",
                table: "Messages",
                column: "MeetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Meets_MeetId",
                table: "Messages",
                column: "MeetId",
                principalTable: "Meets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Meets_MeetId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MeetId",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "MeetId1",
                table: "Messages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Meets",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MeetId1",
                table: "Messages",
                column: "MeetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Meets_MeetId1",
                table: "Messages",
                column: "MeetId1",
                principalTable: "Meets",
                principalColumn: "Id");
        }
    }
}
