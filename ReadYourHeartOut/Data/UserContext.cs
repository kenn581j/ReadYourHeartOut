using Microsoft.EntityFrameworkCore;
using ReadYourHeartOut.Models.Profiles;


namespace ReadYourHeartOut.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAssignment> ServiceAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<ServiceAssignment>().ToTable("ServiceAssignment");

            modelBuilder.Entity<ServiceAssignment>().HasKey(s => new { s.UserID, s.ServiceID });
        }
    }
}
