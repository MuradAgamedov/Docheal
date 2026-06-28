using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "İstifadəçi adı və ya e-poçt daxil edilməlidir")]
        [Display(Name = "İstifadəçi adı / E-poçt")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Şifrə daxil edilməlidir")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [Display(Name = "Məni xatırla")]
        public bool RememberMe { get; set; }
    }
}
