using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.DoctorDtos
{
    public class LocationDto
    {
        [Required(ErrorMessage = "Klinika adı daxil edilməlidir")]
        [StringLength(100, ErrorMessage = "Klinika adı 100 simvoldan çox ola bilməz")]
        public string ClinicName { get; set; }

        [Required(ErrorMessage = "Ünvan daxil edilməlidir")]
        [StringLength(200, ErrorMessage = "Ünvan 200 simvoldan çox ola bilməz")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şəhər daxil edilməlidir")]
        [StringLength(50, ErrorMessage = "Şəhər adı 50 simvoldan çox ola bilməz")]
        public string City { get; set; }

        [StringLength(300, ErrorMessage = "Açıqlama 300 simvoldan çox ola bilməz")]
        public string Description { get; set; }

        [Range(0, 10000, ErrorMessage = "Qiymət 0-10000 arasında olmalıdır")]
        public decimal Price { get; set; }

        [StringLength(50, ErrorMessage = "İş saatları 50 simvoldan çox ola bilməz")]
        public string BusinessHour { get; set; }
    }
}
