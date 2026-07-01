using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.OrderDtos
{
    public class UpdateOrderDto
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Xəstə adı daxil edilməlidir")]
        [Display(Name = "Xəstə Adı")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Blok nömrəsi daxil edilməlidir")]
        [Display(Name = "Blok Nömrəsi")]
        public string BlockNo { get; set; }

        [Required(ErrorMessage = "Mərtəbə nömrəsi daxil edilməlidir")]
        [Display(Name = "Mərtəbə Nömrəsi")]
        public string FloorNo { get; set; }

        [Required(ErrorMessage = "Otaq nömrəsi daxil edilməlidir")]
        [Display(Name = "Otaq Nömrəsi")]
        public string RoomNo { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
