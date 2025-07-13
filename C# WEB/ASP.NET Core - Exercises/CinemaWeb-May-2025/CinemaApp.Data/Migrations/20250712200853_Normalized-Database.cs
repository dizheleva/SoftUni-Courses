#nullable disable

namespace CinemaApp.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class NormalizedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CinemaMovies_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Cinemas_CinemaId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies");

            migrationBuilder.DropTable(
                name: "UserTickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaId_UserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MovieId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CinemaMovieCinemaId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CinemaMovieMovieId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Tickets");

            migrationBuilder.AlterTable(
                name: "UserMovies",
                comment: "User Watchlist entry in the system.");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "UserMovies",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Foreign key to the referenced Movie. Part of the entity composite PK.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserMovies",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Foreign key to the referenced AspNetUser. Part of the entity composite PK.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserMovies",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows if ApplicationUserMovie entry is deleted");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Foreign key to the owner of the ticket",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaMovieId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Foreign key to the Movie projection in a Cinema");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableTickets",
                table: "CinemaMovies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Count of currently available tickets",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Count of currently available tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "CinemaMovies",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Foreign key to the movie",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CinemaId",
                table: "CinemaMovies",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Foreign key to the cinema",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Movie projection with composite key");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CinemaMovies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Movie projection identifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaMovieId_UserId",
                table: "Tickets",
                columns: new[] { "CinemaMovieId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CinemaMovies_CinemaId_MovieId",
                table: "CinemaMovies",
                columns: new[] { "CinemaId", "MovieId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CinemaMovies_CinemaMovieId",
                table: "Tickets",
                column: "CinemaMovieId",
                principalTable: "CinemaMovies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CinemaMovies_CinemaMovieId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaMovieId_UserId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies");

            migrationBuilder.DropIndex(
                name: "IX_CinemaMovies_CinemaId_MovieId",
                table: "CinemaMovies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserMovies");

            migrationBuilder.DropColumn(
                name: "CinemaMovieId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CinemaMovies");

            migrationBuilder.AlterTable(
                name: "UserMovies",
                oldComment: "User Watchlist entry in the system.");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "UserMovies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Foreign key to the referenced Movie. Part of the entity composite PK.");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserMovies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Foreign key to the referenced AspNetUser. Part of the entity composite PK.");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Foreign key to the owner of the ticket");

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaMovieCinemaId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaMovieMovieId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "CinemaMovies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Foreign key to the movie");

            migrationBuilder.AlterColumn<Guid>(
                name: "CinemaId",
                table: "CinemaMovies",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Movie projection with composite key",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Foreign key to the cinema");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableTickets",
                table: "CinemaMovies",
                type: "int",
                nullable: false,
                comment: "Count of currently available tickets",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0,
                oldComment: "Count of currently available tickets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies",
                columns: new[] { "CinemaId", "MovieId" });

            migrationBuilder.CreateTable(
                name: "UserTickets",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTickets", x => new { x.UserId, x.TicketId });
                    table.ForeignKey(
                        name: "FK_UserTickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTickets_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaId_UserId",
                table: "Tickets",
                columns: new[] { "CinemaId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets",
                columns: new[] { "CinemaMovieCinemaId", "CinemaMovieMovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MovieId",
                table: "Tickets",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_TicketId",
                table: "UserTickets",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CinemaMovies_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets",
                columns: new[] { "CinemaMovieCinemaId", "CinemaMovieMovieId" },
                principalTable: "CinemaMovies",
                principalColumns: new[] { "CinemaId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Cinemas_CinemaId",
                table: "Tickets",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
