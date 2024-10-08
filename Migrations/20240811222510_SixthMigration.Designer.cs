﻿// <auto-generated />
using System;
using DalbudakSigorta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DalbudakSigorta.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240811222510_SixthMigration")]
    partial class SixthMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("DalbudakSigorta.Data.Arac", b =>
                {
                    b.Property<int>("AracId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AracMarka")
                        .HasColumnType("TEXT");

                    b.Property<string>("AracModel")
                        .HasColumnType("TEXT");

                    b.Property<int>("AracModelYili")
                        .HasColumnType("INTEGER");

                    b.Property<int>("KaskoDegeri")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("KullaniciId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MotorNo")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlakaIlKodu")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlakaKodu")
                        .HasColumnType("TEXT");

                    b.Property<string>("SasiNo")
                        .HasColumnType("TEXT");

                    b.HasKey("AracId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Araclar");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.AracKayit", b =>
                {
                    b.Property<int>("AracId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AracMarka")
                        .HasColumnType("TEXT");

                    b.Property<string>("AracModel")
                        .HasColumnType("TEXT");

                    b.Property<int>("AracModelYili")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MotorNo")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlakaIlKodu")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlakaKodu")
                        .HasColumnType("TEXT");

                    b.Property<int>("PoliceNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SasiNo")
                        .HasColumnType("TEXT");

                    b.HasKey("AracId");

                    b.HasIndex("PoliceNo")
                        .IsUnique();

                    b.ToTable("AracKayitlari");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Kullanici", b =>
                {
                    b.Property<int>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BaslamaTarihi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Eposta")
                        .HasColumnType("TEXT");

                    b.Property<string>("KullaniciAd")
                        .HasColumnType("TEXT");

                    b.Property<string>("KullaniciSoyad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefon")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("KullaniciId");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Musteri", b =>
                {
                    b.Property<int>("MusteriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DogumTarihi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Eposta")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Il")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ilce")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MusteriAd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MusteriSoyad")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TCKimlik")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.HasKey("MusteriId");

                    b.ToTable("Musteriler");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.OdemeBilgisi", b =>
                {
                    b.Property<int>("OdemeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CVV")
                        .HasColumnType("INTEGER");

                    b.Property<string>("KartIsimSoyisim")
                        .HasColumnType("TEXT");

                    b.Property<string>("KrediKartiNo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("OdemeTarihi")
                        .HasColumnType("TEXT");

                    b.Property<int>("OdemeTutari")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PoliceNo")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SonKullanmaAy")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SonKullanmaYil")
                        .HasColumnType("INTEGER");

                    b.HasKey("OdemeId");

                    b.HasIndex("PoliceNo")
                        .IsUnique();

                    b.ToTable("OdemeBilgileri");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Police", b =>
                {
                    b.Property<int>("PoliceNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BaslangicTarihi")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BitisTarihi")
                        .HasColumnType("TEXT");

                    b.Property<string>("BransKodu")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("KullaniciId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MusteriId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Prim")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TanzimTarihi")
                        .HasColumnType("TEXT");

                    b.HasKey("PoliceNo");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("MusteriId");

                    b.ToTable("Policeler");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Arac", b =>
                {
                    b.HasOne("DalbudakSigorta.Data.Kullanici", null)
                        .WithMany("Araclar")
                        .HasForeignKey("KullaniciId");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.AracKayit", b =>
                {
                    b.HasOne("DalbudakSigorta.Data.Police", "Police")
                        .WithOne("AracKayit")
                        .HasForeignKey("DalbudakSigorta.Data.AracKayit", "PoliceNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Police");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.OdemeBilgisi", b =>
                {
                    b.HasOne("DalbudakSigorta.Data.Police", "Police")
                        .WithOne("OdemeBilgisi")
                        .HasForeignKey("DalbudakSigorta.Data.OdemeBilgisi", "PoliceNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Police");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Police", b =>
                {
                    b.HasOne("DalbudakSigorta.Data.Kullanici", "Kullanici")
                        .WithMany("Policeler")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DalbudakSigorta.Data.Musteri", "Musteri")
                        .WithMany("Policeler")
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");

                    b.Navigation("Musteri");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Kullanici", b =>
                {
                    b.Navigation("Araclar");

                    b.Navigation("Policeler");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Musteri", b =>
                {
                    b.Navigation("Policeler");
                });

            modelBuilder.Entity("DalbudakSigorta.Data.Police", b =>
                {
                    b.Navigation("AracKayit");

                    b.Navigation("OdemeBilgisi");
                });
#pragma warning restore 612, 618
        }
    }
}
