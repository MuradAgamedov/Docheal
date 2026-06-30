using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.DoctorDtos
{
    public class AwardDto
    {
        [Required(ErrorMessage = "Mükafat adı daxil edilməlidir")]
        [StringLength(100, ErrorMessage = "Mükafat adı 100 simvoldan çox ola bilməz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İl daxil edilməlidir")]
        [StringLength(10, ErrorMessage = "İl 10 simvoldan çox ola bilməz")]
        public string Year { get; set; }

        [StringLength(300, ErrorMessage = "Təsvir 300 simvoldan çox ola bilməz")]
        public string Description { get; set; }
    }
}
