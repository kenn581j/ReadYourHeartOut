using Microsoft.EntityFrameworkCore;
using ReadYourHeartOut.Models.Profiles;
using ReadYourHeartOut.Models;

namespace ReadYourHeartOut.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ReadYourHeartOut.Models.Error> Error { get; set; }
    }
}
