using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contest.CompetitionService.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCompetitionVenueType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_competitions_venues_venue_id",
                table: "Competitions");

            migrationBuilder.AlterColumn<Guid>(
                name: "venue_id",
                table: "Competitions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_competitions_venues_venue_id",
                table: "Competitions",
                column: "venue_id",
                principalTable: "Venues",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_competitions_venues_venue_id",
                table: "Competitions");

            migrationBuilder.AlterColumn<Guid>(
                name: "venue_id",
                table: "Competitions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_competitions_venues_venue_id",
                table: "Competitions",
                column: "venue_id",
                principalTable: "Venues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
