using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPDojoWeb.Migrations
{
    /// <inheritdoc />
    public partial class ArtMartials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Armes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Degat = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Armes", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "ArtMartial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtMartial", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Samourais",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Force = table.Column<int>(type: "int", nullable: false),
            //        ArmeId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Samourais", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Samourais_Armes_ArmeId",
            //            column: x => x.ArmeId,
            //            principalTable: "Armes",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateTable(
                name: "ArtMartialSamourai",
                columns: table => new
                {
                    ArtMartialsId = table.Column<int>(type: "int", nullable: false),
                    SamouraisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtMartialSamourai", x => new { x.ArtMartialsId, x.SamouraisId });
                    table.ForeignKey(
                        name: "FK_ArtMartialSamourai_ArtMartial_ArtMartialsId",
                        column: x => x.ArtMartialsId,
                        principalTable: "ArtMartial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtMartialSamourai_Samourais_SamouraisId",
                        column: x => x.SamouraisId,
                        principalTable: "Samourais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtMartialSamourai_SamouraisId",
                table: "ArtMartialSamourai",
                column: "SamouraisId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Samourais_ArmeId",
            //    table: "Samourais",
            //    column: "ArmeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtMartialSamourai");

            migrationBuilder.DropTable(
                name: "ArtMartial");

            migrationBuilder.DropTable(
                name: "Samourais");

            migrationBuilder.DropTable(
                name: "Armes");
        }
    }
}
