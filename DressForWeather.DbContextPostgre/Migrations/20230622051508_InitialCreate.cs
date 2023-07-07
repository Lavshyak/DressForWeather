using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DressForWeather.DbContextPostgre.Migrations
{/*
   protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherStates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TemperatureCelsius = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    WindSpeedMps = table.Column<float>(type: "real", nullable: false),
                    WindDirection = table.Column<int>(type: "integer", nullable: false),
                    HowSunny = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClothesSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothesSets_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clotches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ClothesSetId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clotches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clotches_ClothesSets_ClothesSetId",
                        column: x => x.ClothesSetId,
                        principalTable: "ClothesSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DressReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClothesSetId = table.Column<long>(type: "bigint", nullable: false),
                    UserReporterId = table.Column<long>(type: "bigint", nullable: false),
                    WeatherStateId = table.Column<long>(type: "bigint", nullable: false),
                    Feeling = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DressReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DressReports_ClothesSets_ClothesSetId",
                        column: x => x.ClothesSetId,
                        principalTable: "ClothesSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DressReports_Users_UserReporterId",
                        column: x => x.UserReporterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DressReports_WeatherStates_WeatherStateId",
                        column: x => x.WeatherStateId,
                        principalTable: "WeatherStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClotchesParameterPairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    First = table.Column<string>(type: "text", nullable: false),
                    Second = table.Column<string>(type: "text", nullable: false),
                    ClotchId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClotchesParameterPairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClotchesParameterPairs_Clotches_ClotchId",
                        column: x => x.ClotchId,
                        principalTable: "Clotches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clotches_ClothesSetId",
                table: "Clotches",
                column: "ClothesSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ClotchesParameterPairs_ClotchId",
                table: "ClotchesParameterPairs",
                column: "ClotchId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSets_CreatorId",
                table: "ClothesSets",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DressReports_ClothesSetId",
                table: "DressReports",
                column: "ClothesSetId");

            migrationBuilder.CreateIndex(
                name: "IX_DressReports_UserReporterId",
                table: "DressReports",
                column: "UserReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_DressReports_WeatherStateId",
                table: "DressReports",
                column: "WeatherStateId");
        }
  */
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClotchesParameterPairs");

            migrationBuilder.DropTable(
                name: "ClothTypes");

            migrationBuilder.DropTable(
                name: "DressReports");

            migrationBuilder.DropTable(
                name: "Clotches");

            migrationBuilder.DropTable(
                name: "WeatherStates");

            migrationBuilder.DropTable(
                name: "ClothesSets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
