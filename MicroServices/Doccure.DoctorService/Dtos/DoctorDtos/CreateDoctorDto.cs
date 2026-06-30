using Doccure.DoctorService.Entities;
using System.ComponentModel.DataAnnotations;

namespace Doccure.DoctorService.Dtos.DoctorDtos
{
    public class CreateDoctorDto
    {
        [Required(ErrorMessage = "Ad daxil edilməlidir")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad daxil edilməlidir")]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Şöbə seçilməlidir")]
        public string BranchId { get; set; }

        [Required(ErrorMessage = "E-poçt daxil edilməlidir")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon daxil edilməlidir")]
        [Phone]
        public string Phone { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Haqqında məlumat daxil edilməlidir")]
        [StringLength(1000, MinimumLength = 10)]
        public string About { get; set; }

        [Range(0, 60)]
        public int ExperienceYear { get; set; }

        [Range(0, 10000)]
        public decimal PricePerHour { get; set; }

        public List<EducationDto> Educations { get; set; }
        public List<ExperienceDto> Experiences { get; set; }
        public List<AwardDto> Awards { get; set; }
        public List<LocationDto> Locations { get; set; }
        public List<string> Services { get; set; }
        public List<string> Specializations { get; set; }
    }
}
