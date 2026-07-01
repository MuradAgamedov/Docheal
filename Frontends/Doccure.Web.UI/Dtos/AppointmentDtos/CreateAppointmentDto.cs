using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.AppointmentDtos
{
    public class CreateAppointmentDto
    {
        [Required(ErrorMessage = "Həkim ID daxil edilməlidir")]
        [Display(Name = "Həkim ID")]
        public string DoctorId { get; set; }

        [Required(ErrorMessage = "Xəstə ID daxil edilməlidir")]
        [Display(Name = "Xəstə ID")]
        public string PatientId { get; set; }

        [Required(ErrorMessage = "Görüş tarixi seçilməlidir")]
        [Display(Name = "Görüş Tarixi")]
        public DateTime AppointmentDate { get; set; } = DateTime.Now.AddDays(1);

        [Display(Name = "Qiymət (₼)")]
        public decimal Price { get; set; }
    }
}
