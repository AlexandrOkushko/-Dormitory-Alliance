using Microsoft.EntityFrameworkCore;

namespace DormitoryAlliance.Client.Models
{
    public class DormitoryAllianceDbContext : DbContext
    {
        public DormitoryAllianceDbContext(DbContextOptions<DormitoryAllianceDbContext> options) : base(options) { }

        public DbSet<Dormitory> Dormitories { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}