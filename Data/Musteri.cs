using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace DalbudakSigorta.Data
{
    public class Musteri //bu tanımladığımız class veri tabanındaki bir tabloya karşılık gelir
    {
        // id => primary key
        [Key]
        public int MusteriId { get; set; } //Used to be MusteriId

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik must be exactly 11 characters.")]
        public string? TCKimlik { get; set; }

        [Required]
        public string? MusteriAd { get; set; }

        [Required]
        public string? MusteriSoyad { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Eposta { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }

        [Required]
        public string? Il { get; set; }

        [Required]
        public string? Ilce { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefon must be exactly 11 characters.")]
        public string? Telefon { get; set; }

        public ICollection<Police> Policeler { get; set; } = new List<Police>();

        public string? AdSoyad
        {
            get
            {
                return this.MusteriAd + " " + this.MusteriSoyad;
            }
        }

    }
}