namespace Doccure.PatientService.Dtos.PatientDtos
{
    public class UpdatePatientDto
    {
        public int PatientId { get; set; }
        public string AppUserId { get; set; }
        public IFormFile? ImageFile { get; set; }

        // ── Identity / Şəxsi Məlumatlar ───────────────────────
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string? BloodGroup { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string? ImageUrl { get; set; }

        // ── Tibbi Məlumatlar ──────────────────────────────────
        public string TcKimlikNo { get; set; }
        public string InsuranceType { get; set; }
        public bool Status { get; set; }
    }
}
