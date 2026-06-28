using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad tələb olunur")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad tələb olunur")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "İstifadəçi adı tələb olunur")]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-poçt tələb olunur")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə tələb olunur")]
        [MinLength(6, ErrorMessage = "Şifrə ən az 6 simvol olmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrənin təkrarı tələb olunur")]
        [Compare("Password", ErrorMessage = "Şifrələr uyğun gəlmir")]
        public string ConfirmPassword { get; set; }

        public string? City { get; set; }

        [Required(ErrorMessage = "Doğum tarixi tələb olunur")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Cins seçimi tələb olunur")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Telefon nömrəsi tələb olunur")]
        [Phone(ErrorMessage = "Düzgün telefon nömrəsi daxil edin")]
        public string PhoneNumber { get; set; }
    }
}
