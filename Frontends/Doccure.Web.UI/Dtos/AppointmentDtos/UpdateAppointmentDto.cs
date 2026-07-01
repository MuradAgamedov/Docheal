using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.AppointmentDtos
{
    public class UpdateAppointmentDto
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Görüş tarixi seçilməlidir")]
        [Display(Name = "Görüş Tarixi")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
