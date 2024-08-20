using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DalbudakSigorta.Data
{
    public class OdemeBilgisi
    {
        [Key]
        public int OdemeId { get; set; }

        public int PoliceNo { get; set; } //Foreign Key

        [ForeignKey("PoliceNo")]
        public Police Police { get; set; } = null!;

        public int OdemeTutari { get; set; }
        public DateTime? OdemeTarihi { get; set; }
        public string KrediKartiNo { get; set; } = null!;
        public string? KartIsimSoyisim { get; set; }
        public int? SonKullanmaAy { get; set; }
        public int? SonKullanmaYil { get; set; }
        public int? CVV { get; set; }  

    
    }

}
