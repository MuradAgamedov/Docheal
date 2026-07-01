using System.ComponentModel.DataAnnotations;

namespace Doccure.MarketService.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Məhsul adı daxil edilməlidir")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Məhsul adı 2 ilə 100 simvol arasında olmalıdır")]
        public string ProductName { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Qiymət mənfi ola bilməz")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Kateqoriya daxil edilməlidir")]
        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public bool Status { get; set; }
    }
}
