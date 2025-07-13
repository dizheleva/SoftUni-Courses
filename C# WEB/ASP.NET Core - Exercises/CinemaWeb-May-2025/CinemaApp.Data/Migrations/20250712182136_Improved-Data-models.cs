#nullable disable

namespace CinemaApp.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class ImprovedDatamodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaId",
                table: "Tickets");

            migrationBuilder.AlterTable(
                name: "Tickets",
                comment: "Ticket in the system");

            migrationBuilder.AlterTable(
                name: "CinemaMovies",
                comment: "Movie projection in a cinema in the system");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Ticket price",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Ticket identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Cinemas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates if the cinema is deleted",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates if the cinema is deleted");

            migrationBuilder.AlterColumn<string>(
                name: "Showtimes",
                table: "CinemaMovies",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "00000",
                comment: "String indicating the showtime of the Movie projection",
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldDefaultValue: "00000");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CinemaMovies",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows if the movie projection in a cinema is active",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "AvailableTickets",
                table: "CinemaMovies",
                type: "int",
                nullable: false,
                comment: "Count of currently available tickets",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CinemaId",
                table: "CinemaMovies",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Movie projection with composite key",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaId_UserId",
                table: "Tickets",
                columns: new[] { "CinemaId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets",
                columns: new[] { "CinemaMovieCinemaId", "CinemaMovieMovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CinemaMovies_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets",
                columns: new[] { "CinemaMovieCinemaId", "CinemaMovieMovieId" },
                principalTable: "CinemaMovies",
                principalColumns: new[] { "CinemaId", "MovieId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CinemaMovies_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaId_UserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaMovieCinemaId_CinemaMovieMovieId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CinemaMovieCinemaId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CinemaMovieMovieId",
                table: "Tickets");

            migrationBuilder.AlterTable(
                name: "Tickets",
                oldComment: "Ticket in the system");

            migrationBuilder.AlterTable(
                name: "CinemaMovies",
                oldComment: "Movie projection in a cinema in the system");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Ticket price");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Ticket identifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Cinemas",
                type: "bit",
                nullable: false,
                comment: "Indicates if the cinema is deleted",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Indicates if the cinema is deleted");

            migrationBuilder.AlterColumn<string>(
                name: "Showtimes",
                table: "CinemaMovies",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "00000",
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldDefaultValue: "00000",
                oldComment: "String indicating the showtime of the Movie projection");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CinemaMovies",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Shows if the movie projection in a cinema is active");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableTickets",
                table: "CinemaMovies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Count of currently available tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "CinemaId",
                table: "CinemaMovies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Movie projection with composite key");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaId",
                table: "Tickets",
                column: "CinemaId");
        }
    }
}
