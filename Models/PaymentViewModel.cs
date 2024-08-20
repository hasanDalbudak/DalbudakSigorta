namespace DalbudakSigorta.Models
{
    public class PaymentViewModel
    {
        public int PoliceNo { get; set; }          // Required to link the payment to a specific police
        public int OdemeTutari { get; set; }       // The amount to be paid
        public string KrediKartiNo { get; set; } = null!;  // Credit card number
        public string? KartIsimSoyisim { get; set; }  // Name on the card
        public int? SonKullanmaAy { get; set; }    // Expiry month
        public int? SonKullanmaYil { get; set; }   // Expiry year
        public int? CVV { get; set; }              // CVV code
    }
}
