using System.ComponentModel.DataAnnotations;

namespace DalbudakSigorta.Data
{
    public class Arac
    {
        [Key]
        public int AracId { get; set; }
        public string? PlakaIlKodu { get; set; }
        public string? PlakaKodu { get; set; }
        public string? AracMarka { get; set; }
        public string? AracModel { get; set; }
        public int AracModelYili { get; set; }
        public string? MotorNo { get; set; } //10 Karakter
        public string? SasiNo { get; set; } //11 Karakter
        public int KaskoDegeri { get; set; }
        
        //public Musteri Musteri { get; set; } = null!; // Müsteri tablosu ile bir ilişki kurmak için oluşturduk. Navigation Property. 
        //public Kullanici Kullanici { get; set; } = null!; //Arac tablosunda kullanıcı id olacak bu satır sayesinde. Yani 1 poliçeyi yalnızda 1 kullanıcı kesebilir. Birden fazla vermesini isteseydik List olarak kayıt ederdik.
        //public ICollection<AracKayit> AracKayitlari { get; set; } = new List<AracKayit>();

    }

}