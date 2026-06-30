using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Doccure.Web.UI.Dtos.DoctorDtos
{
    public class UpdateDoctorDto
    {
        [Required]
        public string DoctorId { get; set; }

        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 simvol arasında olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad daxil edilməlidir")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 simvol arasında olmalıdır")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Şöbə seçilməlidir")]
        public string BranchId { get; set; }

        [Required(ErrorMessage = "E-poçt daxil edilməlidir")]
        [EmailAddress(ErrorMessage = "E-poçt formatı düzgün deyil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon daxil edilməlidir")]
        [Phone(ErrorMessage = "Telefon nömrəsi formatı düzgün deyil")]
        public string Phone { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Haqqında məlumat daxil edilməlidir")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Haqqında bölməsi 10-1000 simvol arasında olmalıdır")]
        public string About { get; set; }

        [Range(0, 60, ErrorMessage = "Təcrübə ili 0-60 arasında olmalıdır")]
        public int ExperienceYear { get; set; }

        [Range(0, 10000, ErrorMessage = "Qiymət 0-10000 arasında olmalıdır")]
        public decimal PricePerHour { get; set; }

        public List<EducationDto> Educations { get; set; } = new List<EducationDto>();
        public List<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();
        public List<AwardDto> Awards { get; set; } = new List<AwardDto>();
        public List<LocationDto> Locations { get; set; } = new List<LocationDto>();
        public List<string> Services { get; set; } = new List<string>();
        public List<string> Specializations { get; set; } = new List<string>();
    }
}
