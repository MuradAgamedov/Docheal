namespace Doccure.PatientService.Dtos.PatientDtos
{
    public class CreatePatientDto
    {
        // User Registration Fields
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? City { get; set; }
        public string? ImageUrl { get; set; }

        // Patient Medical Fields
        public string TcKimlikNo { get; set; }
        public string InsuranceType { get; set; }
        public bool Status { get; set; }
    }
}
