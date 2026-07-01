using Doccure.Web.UI.Dtos.AppointmentDtos;
using Doccure.Web.UI.Dtos.BranchDtos;
using Doccure.Web.UI.Dtos.DoctorDtos;
using Doccure.Web.UI.Dtos.PatientDtos;

namespace Doccure.Web.UI.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int DoctorCount { get; set; }
        public int AppointmentCount { get; set; }
        public int BranchCount { get; set; }
        public int PatientCount { get; set; }

        public List<ResultAppointmentDto> RecentAppointments { get; set; } = new();
        public List<ResultDoctorDto> TopDoctors { get; set; } = new();
        public List<ResultPatientDto> RecentPatients { get; set; } = new();
        public List<ResultBranchDto> Branches { get; set; } = new();

        public decimal TotalRevenue => RecentAppointments.Sum(a => a.Price);
    }
}
