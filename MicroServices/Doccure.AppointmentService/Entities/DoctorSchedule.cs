using System;

namespace Doccure.AppointmentService.Entities
{
    public class DoctorSchedule
    {
        public int DoctorScheduleId { get; set; }
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public int? AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
