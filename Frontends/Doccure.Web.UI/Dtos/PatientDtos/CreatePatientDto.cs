using System;
using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.PatientDtos
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 simvol arasında olmalıdır")]
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required(ErrorMessage = "Soyad daxil edilməlidir")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 simvol arasında olmalıdır")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "İstifadəçi adı daxil edilməlidir")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "İstifadəçi adı 3-50 simvol arasında olmalıdır")]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-poçt daxil edilməlidir")]
        [EmailAddress(ErrorMessage = "E-poçt formatı düzgün deyil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə daxil edilməlidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Telefon daxil edilməlidir")]
        [Phone(ErrorMessage = "Telefon nömrəsi formatı düzgün deyil")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Doğum tarixi daxil edilməlidir")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Cins seçilməlidir")]
        public string Gender { get; set; }

        public string? BloodGroup { get; set; }

        public string? City { get; set; }

        [Required(ErrorMessage = "T.C. Kimlik No daxil edilməlidir")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Kimlik No tam 11 simvol olmalıdır")]
        public string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Sığorta növü seçilməlidir")]
        public string InsuranceType { get; set; }

        public string? ImageUrl { get; set; }

        public bool Status { get; set; } = true;
    }
}
