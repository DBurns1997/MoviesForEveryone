using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesForEveryone.Migrations
{
    public partial class MoviesForEveryone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Theaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    avgClean = table.Column<float>(type: "real", nullable: false),
                    avgConc = table.Column<float>(type: "real", nullable: false),
                    avgArcade = table.Column<float>(type: "real", nullable: false),
                    avgViewing = table.Column<float>(type: "real", nullable: false),
                    totalAvg = table.Column<float>(type: "real", nullable: false)
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
                    cleanlinessRating = table.Column<float>(type: "real", nullable: false),
                    concessionsRating = table.Column<float>(type: "real", nullable: false),
                    arcadeRating = table.Column<float>(type: "real", nullable: false),
                    experienceRating = table.Column<float>(type: "real", nullable: false),
                    reviewAvgScore = table.Column<float>(type: "real", nullable: false),
                    cleanlinessReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    concessionsReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arcadeReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    experienceReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheaterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.reviewKey);
                    table.ForeignKey(
                        name: "FK_Reviews_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TheaterId",
                table: "Reviews",
                column: "TheaterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Theaters");
        }
    }
}
