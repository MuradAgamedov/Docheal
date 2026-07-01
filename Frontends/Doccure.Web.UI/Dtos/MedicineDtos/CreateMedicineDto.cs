using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.MedicineDtos
{
    public class CreateMedicineDto
    {
        [Required(ErrorMessage = "Dərman adı daxil edilməlidir")]
        [Display(Name = "Dərman Adı")]
        public string MedicineName { get; set; }

        [Required(ErrorMessage = "Barkod daxil edilməlidir")]
        [Display(Name = "Barkod")]
        public string Barcode { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok mənfi ola bilməz")]
        [Display(Name = "Stok Miqdarı")]
        public int Stock { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Kritik stok mənfi ola bilməz")]
        [Display(Name = "Kritik Stok Səviyyəsi")]
        public int CriticalStockLevel { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Qiymət mənfi ola bilməz")]
        [Display(Name = "Vahid Qiymət (₼)")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Son istifadə tarixi daxil edilməlidir")]
        [Display(Name = "Son İstifadə Tarixi")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
        public string? ImageUrl { get; set; }
    }
}
