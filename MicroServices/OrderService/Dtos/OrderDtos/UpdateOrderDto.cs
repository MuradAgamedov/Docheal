using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos.OrderDtos
{
    public class UpdateOrderDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Xəstə adı daxil edilməlidir")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Blok nömrəsi daxil edilməlidir")]
        public string BlockNo { get; set; }

        [Required(ErrorMessage = "Mərtəbə nömrəsi daxil edilməlidir")]
        public string FloorNo { get; set; }

        [Required(ErrorMessage = "Otaq nömrəsi daxil edilməlidir")]
        public string RoomNo { get; set; }

        public string Status { get; set; }
    }
}
