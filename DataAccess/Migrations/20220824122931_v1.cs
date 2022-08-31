using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YemekAppMalzemeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekAppMalzemeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YemekAppRoller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekAppRoller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YemekAppUlkeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekAppUlkeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YemekAppTarifler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Tarif = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    MalzemeId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekAppTarifler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YemekAppTarifler_YemekAppMalzemeler_MalzemeId",
                        column: x => x.MalzemeId,
                        principalTable: "YemekAppMalzemeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YemekAppKullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekAppKullanicilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YemekAppKullanicilar_YemekAppRoller_RolId",
                        column: x => x.RolId,
                        principalTable: "YemekAppRoller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YemekAppSehirler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UlkeId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekAppSehirler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YemekAppSehirler_YemekAppUlkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "YemekAppUlkeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YemekAppKullaniciDetaylari",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Cinsiyet = table.Column<int>(type: "int", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    UlkeId = table.Column<int>(type: "int", nullable: false),
                    SehirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YemekAppKullaniciDetaylari", x => x.KullaniciId);
                    table.ForeignKey(
                        name: "FK_YemekAppKullaniciDetaylari_YemekAppKullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "YemekAppKullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekAppKullaniciDetaylari_YemekAppSehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "YemekAppSehirler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YemekAppKullaniciDetaylari_YemekAppUlkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "YemekAppUlkeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppKullaniciDetaylari_Eposta",
                table: "YemekAppKullaniciDetaylari",
                column: "Eposta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppKullaniciDetaylari_SehirId",
                table: "YemekAppKullaniciDetaylari",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppKullaniciDetaylari_UlkeId",
                table: "YemekAppKullaniciDetaylari",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppKullanicilar_KullaniciAdi",
                table: "YemekAppKullanicilar",
                column: "KullaniciAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppKullanicilar_RolId",
                table: "YemekAppKullanicilar",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppSehirler_UlkeId",
                table: "YemekAppSehirler",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppTarifler_Adi",
                table: "YemekAppTarifler",
                column: "Adi");

            migrationBuilder.CreateIndex(
                name: "IX_YemekAppTarifler_MalzemeId",
                table: "YemekAppTarifler",
                column: "MalzemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YemekAppKullaniciDetaylari");

            migrationBuilder.DropTable(
                name: "YemekAppTarifler");

            migrationBuilder.DropTable(
                name: "YemekAppKullanicilar");

            migrationBuilder.DropTable(
                name: "YemekAppSehirler");

            migrationBuilder.DropTable(
                name: "YemekAppMalzemeler");

            migrationBuilder.DropTable(
                name: "YemekAppRoller");

            migrationBuilder.DropTable(
                name: "YemekAppUlkeler");
        }
    }
}
