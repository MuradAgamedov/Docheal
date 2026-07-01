using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos.OrderDetailDtos
{
    public class CreateOrderDetailDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Məhsul adı daxil edilməlidir")]
        public string ProductName { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Qiymət mənfi ola bilməz")]
        public decimal UnitPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Miqdar ən az 1 olmalıdır")]
        public int Quantity { get; set; }
    }
}
