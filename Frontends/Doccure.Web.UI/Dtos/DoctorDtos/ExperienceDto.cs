using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.DoctorDtos
{
    public class ExperienceDto
    {
        [Required(ErrorMessage = "Klinika adı daxil edilməlidir")]
        [StringLength(100, ErrorMessage = "Klinika adı 100 simvoldan çox ola bilməz")]
        public string ClinicName { get; set; }

        [Required(ErrorMessage = "İl aralığı daxil edilməlidir")]
        [StringLength(30, ErrorMessage = "İl aralığı 30 simvoldan çox ola bilməz")]
        public string YearRange { get; set; }
    }
}
