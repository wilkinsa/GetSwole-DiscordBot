using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddColumnsToCampaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "PostId",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CampaignId",
                table: "Users",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Campaigns_CampaignId",
                table: "Users",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Campaigns_CampaignId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CampaignId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Campaigns");
        }
    }
}
