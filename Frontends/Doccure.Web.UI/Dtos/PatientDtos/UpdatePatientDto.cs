using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.PatientDtos
{
    public class UpdatePatientDto
    {
        [Required]
        public int PatientId { get; set; }

        public string AppUserId { get; set; }
        public IFormFile? ImageFile { get; set; }
        // ── Şəxsi Məlumatlar (Identity) ───────────────────────
        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad daxil edilməlidir")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-poçt daxil edilməlidir")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt formatı daxil edin")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string? BloodGroup { get; set; }

        public DateTime? BirthDate { get; set; }

        public string City { get; set; }

        // ── Tibbi Məlumatlar ──────────────────────────────────
        [Required(ErrorMessage = "T.C. Kimlik No daxil edilməlidir")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Kimlik No tam 11 simvol olmalıdır")]
        public string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Sığorta növü seçilməlidir")]
        public string InsuranceType { get; set; }

        public string? ImageUrl { get; set; }

        public bool Status { get; set; }
    }
}
