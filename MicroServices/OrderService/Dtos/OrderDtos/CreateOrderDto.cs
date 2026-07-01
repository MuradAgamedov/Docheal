using System.ComponentModel.DataAnnotations;
using OrderService.Dtos.OrderDetailDtos;

namespace OrderService.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Xəstə adı daxil edilməlidir")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Blok nömrəsi daxil edilməlidir")]
        public string BlockNo { get; set; }

        [Required(ErrorMessage = "Mərtəbə nömrəsi daxil edilməlidir")]
        public string FloorNo { get; set; }

        [Required(ErrorMessage = "Otaq nömrəsi daxil edilməlidir")]
        public string RoomNo { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Gözləyir";

        public List<CreateOrderDetailDto> OrderDetails { get; set; }
    }
}
