using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesForEveryone.Migrations
{
    public partial class MoviesForEveryone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opinions",
                columns: table => new
                {
                    opinionKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    liked = table.Column<bool>(type: "bit", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.opinionKey);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    settingsKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    radiusSetting = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.settingsKey);
                });

            migrationBuilder.CreateTable(
                name: "Theaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    theaterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    avgClean = table.Column<double>(type: "float", nullable: false),
                    avgConc = table.Column<double>(type: "float", nullable: false),
                    avgArcade = table.Column<double>(type: "float", nullable: false),
                    avgViewing = table.Column<double>(type: "float", nullable: false),
                    totalAvg = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    reviewKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cleanlinessRating = table.Column<double>(type: "float", nullable: false),
                    concessionsRating = table.Column<double>(type: "float", nullable: false),
                    arcadeRating = table.Column<double>(type: "float", nullable: false),
                    experienceRating = table.Column<double>(type: "float", nullable: false),
                    reviewAvgScore = table.Column<double>(type: "float", nullable: false),
                    cleanlinessReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    concessionsReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arcadeReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    experienceReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    helpfulRatingPercent = table.Column<double>(type: "float", nullable: false),
                    numberHelpfulVotes = table.Column<int>(type: "int", nullable: false),
                    totalHelpRates = table.Column<int>(type: "int", nullable: false),
                    TheaterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.reviewKey);
                    table.ForeignKey(
                        name: "FK_Reviews_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TheaterId",
                table: "Reviews",
                column: "TheaterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opinions");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Theaters");
        }
    }
}
