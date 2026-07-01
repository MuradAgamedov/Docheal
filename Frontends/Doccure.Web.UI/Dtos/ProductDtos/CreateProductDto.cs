using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Məhsul adı daxil edilməlidir")]
        [Display(Name = "Məhsul Adı")]
        public string ProductName { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Qiymət mənfi ola bilməz")]
        [Display(Name = "Qiymət (₼)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Kateqoriya daxil edilməlidir")]
        [Display(Name = "Kateqoriya")]
        public string Category { get; set; }

        public string? ImageUrl { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
    }
}
