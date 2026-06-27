using Doccure.PrescriptionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doccure.PrescriptionService.Context
{
    public class PrescriptionContext : DbContext
    {
        public PrescriptionContext(DbContextOptions<PrescriptionContext> options):base(options)
        {
        }

        DbSet<Prescription> Prescriptions { get; set; }
        DbSet<PrescriptionItem> PrescriptionItems { get; set; }
    }
}
