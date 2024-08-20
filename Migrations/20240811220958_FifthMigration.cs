using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalbudakSigorta.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OdemeBilgileri_Policeler_PoliceNo",
                table: "OdemeBilgileri");

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "Policeler",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PoliceNo",
                table: "OdemeBilgileri",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policeler_KullaniciId",
                table: "Policeler",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_OdemeBilgileri_Policeler_PoliceNo",
                table: "OdemeBilgileri",
                column: "PoliceNo",
                principalTable: "Policeler",
                principalColumn: "PoliceNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_Kullanicilar_KullaniciId",
                table: "Policeler",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OdemeBilgileri_Policeler_PoliceNo",
                table: "OdemeBilgileri");

            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_Kullanicilar_KullaniciId",
                table: "Policeler");

            migrationBuilder.DropIndex(
                name: "IX_Policeler_KullaniciId",
                table: "Policeler");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Policeler");

            migrationBuilder.AlterColumn<int>(
                name: "PoliceNo",
                table: "OdemeBilgileri",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_OdemeBilgileri_Policeler_PoliceNo",
                table: "OdemeBilgileri",
                column: "PoliceNo",
                principalTable: "Policeler",
                principalColumn: "PoliceNo");
        }
    }
}
