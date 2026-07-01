using System;
using System.ComponentModel.DataAnnotations;

namespace Doccure.PharmacyService.Dtos.MedicineDtos
{
    public class CreateMedicineDto
    {
        [Required(ErrorMessage = "Dərman adı daxil edilməlidir")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Dərman adı 2 ilə 100 simvol arasında olmalıdır")]
        public string MedicineName { get; set; }

        [Required(ErrorMessage = "Barkod daxil edilməlidir")]
        [StringLength(50, ErrorMessage = "Barkod ən çox 50 simvol ola bilər")]
        public string Barcode { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok mənfi ola bilməz")]
        public int Stock { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Kritik stok mənfi ola bilməz")]
        public int CriticalStockLevel { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Qiymət mənfi ola bilməz")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Son istifadə tarixi daxil edilməlidir")]
        public DateTime ExpirationDate { get; set; }

        public bool Status { get; set; } = true;
        public string? ImageUrl { get; set; }
    }
}
