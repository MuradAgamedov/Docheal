namespace Doccure.AppointmentService.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string? BranchId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public AppointmentDetail AppointmentDetail { get; set; }
        public List<DoctorSchedule> DoctorSchedules { get; set; }

    }
}
