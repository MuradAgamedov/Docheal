namespace Doccure.NurseService.Dtos.NurseDtos
{
    public class GetByIdNurseDto
    {
        public int NurseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Unit { get; set; }
        public string Branch { get; set; }
        public string Shift { get; set; }
        public string Status { get; set; }
        public int Experience { get; set; }
        public decimal Rating { get; set; }
        public int PatientCount { get; set; }
        public string Skills { get; set; }
        public int Performance { get; set; }
        public int Attendance { get; set; }
    }
}
