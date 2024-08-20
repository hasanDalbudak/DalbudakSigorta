using System.ComponentModel.DataAnnotations;

namespace DalbudakSigorta.Data
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }

        public string? KullaniciAd { get; set; }

        public string? KullaniciSoyad { get; set; }

        public string? UserName { get; set; }
          
        public string? Eposta { get; set; } 

        public string? Password { get; set; }

        public string? Telefon { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime BaslamaTarihi { get; set; } 

        public ICollection<Arac> Araclar { get; set; } = new List<Arac>(); // One to many

        public ICollection<Police> Policeler { get; set; } = new List<Police>();

    }
}