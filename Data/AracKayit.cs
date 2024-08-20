using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DalbudakSigorta.Data
{
    public class AracKayit
    {
        [Key]
        public int AracId { get; set; }

        public int PoliceNo { get; set; } // Foreign Key

        [ForeignKey("PoliceNo")]
        public Police Police { get; set; } = null!;

        public string? PlakaIlKodu { get; set; }
        public string? PlakaKodu { get; set; }
        public string? AracMarka { get; set; }
        public string? AracModel { get; set; }
        public int AracModelYili { get; set; }
        public string? MotorNo { get; set; }
        public string? SasiNo { get; set; }
        
        //public Musteri Musteri { get; set; } = null!; // Müsteri tablosu ile bir ilişki kurmak için oluşturduk. Navigation Property. 
        //public Arac Arac { get; set; } = null!; // Arac tablosuna erişmek için oluşturduk. Navigation Property.
    }
}