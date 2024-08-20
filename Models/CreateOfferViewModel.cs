namespace DalbudakSigorta.Models
{
    public class CreateOfferViewModel
    {
        public int PoliceNo { get; set; } // Add this property
        public string BransKodu { get; set; } = string.Empty;
        public int MusteriId { get; set; }

        public string? PlakaIlKodu { get; set; }
        public string? PlakaKodu { get; set; }
        public string? AracMarka { get; set; }
        public string? AracModel { get; set; }
        public int AracModelYili { get; set; }

        // New properties for displaying the offer details
        public decimal KaskoDegeri { get; set; }
        public decimal TeklifTutari { get; set; }
        public bool IsOfferGenerated { get; set; } // Indicates if "Teklif Al" has been clicked
    }
}
