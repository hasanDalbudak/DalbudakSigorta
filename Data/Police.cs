using System.ComponentModel.DataAnnotations;

namespace DalbudakSigorta.Data
{
    public class Police
    {
        [Key]
        public int PoliceNo { get; set; } //Primary

        [Required]
        public int MusteriId { get; set; } // Foreign Key
        public Musteri? Musteri { get; set; }  // Navigation Propery

        public string? Status { get; set; } //Teklif (T) veya Poliçe(P)

        [Required]
        public string BransKodu { get; set; } = string.Empty;

        public decimal Prim { get; set; }

        public int KullaniciId { get; set; } //User tablosundaki UserId Burasının Foreign Key'i. Çok gerek olmaz.
        public Kullanici? Kullanici { get; set; }

        public DateTime TanzimTarihi { get; set; } //Aslında işlemin yapılıp teklifin sunulduğu tarih, DateTime.Today veya DateTime.Now

        public DateTime BaslangicTarihi { get; set; } //DateTime.Today

        public DateTime BitisTarihi { get; set; } //VadeBaslangıcTarihi + 15 Gün

        public AracKayit? AracKayit { get; set; } = null!;

        public OdemeBilgisi? OdemeBilgisi { get; set; } = null!;


    }
}