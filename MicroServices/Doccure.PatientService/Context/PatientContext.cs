using Doccure.PatientService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doccure.PatientService.Context
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
    }
}
