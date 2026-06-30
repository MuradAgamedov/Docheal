using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.DoctorDtos
{
    public class EducationDto
    {
        [Required(ErrorMessage = "Məktəb adı daxil edilməlidir")]
        [StringLength(100, ErrorMessage = "Məktəb adı 100 simvoldan çox ola bilməz")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Dərəcə daxil edilməlidir")]
        [StringLength(50, ErrorMessage = "Dərəcə 50 simvoldan çox ola bilməz")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "İl daxil edilməlidir")]
        [StringLength(20, ErrorMessage = "İl 20 simvoldan çox ola bilməz")]
        public string Year { get; set; }
    }
}
