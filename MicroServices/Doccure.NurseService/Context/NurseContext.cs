using Doccure.NurseService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doccure.NurseService.Context
{
    public class NurseContext : DbContext
    {
        public NurseContext(DbContextOptions<NurseContext> options) : base(options) { }

        public DbSet<Nurse> Nurses { get; set; }
    }
}
