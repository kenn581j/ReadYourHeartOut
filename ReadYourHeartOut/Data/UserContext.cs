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
    }
}
