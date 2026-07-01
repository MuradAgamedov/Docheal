using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.NurseDtos
{
    public class CreateNurseDto
    {
        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad daxil edilməlidir")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Display(Name = "Bölmə")]
        public string Unit { get; set; }

        [Display(Name = "Filial")]
        public string Branch { get; set; }

        [Display(Name = "Növbə")]
        public string Shift { get; set; } = "Gündüz";

        [Display(Name = "Status")]
        public string Status { get; set; } = "Növbədə";

        [Display(Name = "Təcrübə (il)")]
        public int Experience { get; set; }

        [Display(Name = "Bal")]
        public decimal Rating { get; set; } = 4.5m;

        [Display(Name = "Xəstə Sayı")]
        public int PatientCount { get; set; }

        [Display(Name = "Bacarıqlar (vergüllə ayırın)")]
        public string Skills { get; set; }

        [Display(Name = "Performans (%)")]
        public int Performance { get; set; } = 85;

        [Display(Name = "Davamiyyət (%)")]
        public int Attendance { get; set; } = 90;
    }
}
