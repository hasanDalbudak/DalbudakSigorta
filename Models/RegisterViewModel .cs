using System.ComponentModel.DataAnnotations;

namespace DalbudakSigorta.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "UserName")]
        public string? UserName { get; set; }   

        // [Required]
        // [Display(Name = "Ad Soyad")]
        // public string? Name { get; set; }   

        [Required]
        [Display(Name = "İsim")]
        public string? KullaniciAd { get; set; }

        [Required]
        [Display(Name = "Soyisim")]
        public string? KullaniciSoyad { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? Eposta { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parola eşleşmiyor")]
        [Display(Name = "Parola Tekrar")]
        public string? ConfirmPassword { get; set; }



        
    }
}