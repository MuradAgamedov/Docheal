using Microsoft.EntityFrameworkCore;
using Doccure.PharmacyService.Entities;

namespace Doccure.PharmacyService.Context
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options)
        {
        }
        public DbSet<Medicine> Medicines { get; set; }
    }
}
