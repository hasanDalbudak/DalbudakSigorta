﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalbudakSigorta.Migrations
{
    /// <inheritdoc />
    public partial class SixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_Kullanicilar_KullaniciId",
                table: "Policeler");

            migrationBuilder.DropColumn(
                name: "OnaylayanId",
                table: "Policeler");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Policeler",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_Kullanicilar_KullaniciId",
                table: "Policeler",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_Kullanicilar_KullaniciId",
                table: "Policeler");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Policeler",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "OnaylayanId",
                table: "Policeler",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_Kullanicilar_KullaniciId",
                table: "Policeler",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId");
        }
    }
}
