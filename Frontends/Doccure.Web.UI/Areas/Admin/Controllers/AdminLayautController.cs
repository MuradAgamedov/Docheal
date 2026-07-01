using Doccure.Web.UI.Services.AppointmentServices;
using Doccure.Web.UI.Services.BranchServices;
using Doccure.Web.UI.Services.DoctorServices;
using Doccure.Web.UI.Services.PatientServices;
using Doccure.Web.UI.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLayautController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly IBranchService _branchService;
        private readonly IPatientService _patientService;

        public AdminLayautController(
            IDoctorService doctorService,
            IAppointmentService appointmentService,
            IBranchService branchService,
            IPatientService patientService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _branchService = branchService;
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel();

            try
            {
                var doctors = await _doctorService.GetAllAsync();
                vm.DoctorCount = doctors.Count;
                vm.TopDoctors = doctors.Take(5).ToList();
            }
            catch { }

            try
            {
                var appointments = await _appointmentService.GetAllAsync();
                vm.AppointmentCount = appointments.Count;
                vm.RecentAppointments = appointments
                    .OrderByDescending(a => a.AppointmentDate)
                    .Take(5)
                    .ToList();
            }
            catch { }

            try
            {
                var branches = await _branchService.GetAllAsync();
                vm.BranchCount = branches.Count;
                vm.Branches = branches;
            }
            catch { }

            try
            {
                var patients = await _patientService.GetAllAsync();
                vm.PatientCount = patients.Count;
                vm.RecentPatients = patients
                    .OrderByDescending(p => p.CreatedDate)
                    .Take(5)
                    .ToList();
            }
            catch { }

            return View(vm);
        }
    }
}
