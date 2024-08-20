using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalbudakSigorta.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KullaniciAd = table.Column<string>(type: "TEXT", nullable: true),
                    KullaniciSoyad = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Eposta = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    MusteriId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TCKimlik = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    MusteriAd = table.Column<string>(type: "TEXT", nullable: false),
                    MusteriSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    Eposta = table.Column<string>(type: "TEXT", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Il = table.Column<string>(type: "TEXT", nullable: false),
                    Ilce = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.MusteriId);
                });

            migrationBuilder.CreateTable(
                name: "Araclar",
                columns: table => new
                {
                    AracId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlakaIlKodu = table.Column<string>(type: "TEXT", nullable: true),
                    PlakaKodu = table.Column<string>(type: "TEXT", nullable: true),
                    AracMarka = table.Column<string>(type: "TEXT", nullable: true),
                    AracModel = table.Column<string>(type: "TEXT", nullable: true),
                    AracModelYili = table.Column<int>(type: "INTEGER", nullable: false),
                    MotorNo = table.Column<string>(type: "TEXT", nullable: true),
                    SasiNo = table.Column<string>(type: "TEXT", nullable: true),
                    KaskoDegeri = table.Column<int>(type: "INTEGER", nullable: false),
                    KullaniciId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Araclar", x => x.AracId);
                    table.ForeignKey(
                        name: "FK_Araclar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId");
                });

            migrationBuilder.CreateTable(
                name: "Policeler",
                columns: table => new
                {
                    PoliceNo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusteriId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    BransKodu = table.Column<string>(type: "TEXT", nullable: false),
                    Prim = table.Column<decimal>(type: "TEXT", nullable: false),
                    OnaylayanId = table.Column<int>(type: "INTEGER", nullable: false),
                    TanzimTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policeler", x => x.PoliceNo);
                    table.ForeignKey(
                        name: "FK_Policeler_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AracKayitlari",
                columns: table => new
                {
                    AracId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PoliceNo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlakaIlKodu = table.Column<string>(type: "TEXT", nullable: true),
                    PlakaKodu = table.Column<string>(type: "TEXT", nullable: true),
                    AracMarka = table.Column<string>(type: "TEXT", nullable: true),
                    AracModel = table.Column<string>(type: "TEXT", nullable: true),
                    AracModelYili = table.Column<int>(type: "INTEGER", nullable: false),
                    MotorNo = table.Column<string>(type: "TEXT", nullable: true),
                    SasiNo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AracKayitlari", x => x.AracId);
                    table.ForeignKey(
                        name: "FK_AracKayitlari_Policeler_PoliceNo",
                        column: x => x.PoliceNo,
                        principalTable: "Policeler",
                        principalColumn: "PoliceNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OdemeBilgileri",
                columns: table => new
                {
                    OdemeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PoliceNo = table.Column<int>(type: "INTEGER", nullable: true),
                    OdemeTutari = table.Column<int>(type: "INTEGER", nullable: false),
                    OdemeTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KrediKartiNo = table.Column<int>(type: "INTEGER", nullable: false),
                    KartIsimSoyisim = table.Column<string>(type: "TEXT", nullable: true),
                    SonKullanmaAy = table.Column<int>(type: "INTEGER", nullable: false),
                    SonKullanmaYil = table.Column<int>(type: "INTEGER", nullable: false),
                    CVV = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdemeBilgileri", x => x.OdemeId);
                    table.ForeignKey(
                        name: "FK_OdemeBilgileri_Policeler_PoliceNo",
                        column: x => x.PoliceNo,
                        principalTable: "Policeler",
                        principalColumn: "PoliceNo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AracKayitlari_PoliceNo",
                table: "AracKayitlari",
                column: "PoliceNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_KullaniciId",
                table: "Araclar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_OdemeBilgileri_PoliceNo",
                table: "OdemeBilgileri",
                column: "PoliceNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policeler_MusteriId",
                table: "Policeler",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AracKayitlari");

            migrationBuilder.DropTable(
                name: "Araclar");

            migrationBuilder.DropTable(
                name: "OdemeBilgileri");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Policeler");

            migrationBuilder.DropTable(
                name: "Musteriler");
        }
    }
}
