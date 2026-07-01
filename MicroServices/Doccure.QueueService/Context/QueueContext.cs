using Doccure.QueueService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doccure.QueueService.Context
{
    public class QueueContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-U7NODTP;initial catalog=DoccureQueueDb;integrated security=true;TrustServerCertificate=True");


        }

        public DbSet<PatientQueue> PatientQueues { get; set; }
    }
}
