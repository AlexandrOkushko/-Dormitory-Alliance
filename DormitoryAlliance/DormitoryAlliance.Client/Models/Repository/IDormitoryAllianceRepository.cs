using System.Linq;
using DormitoryAlliance.Client.Areas.Identity.Data;

namespace DormitoryAlliance.Client.Models.Repository
{
    public interface IDormitoryAllianceRepository
    {
        IQueryable<Dormitory> Dormitories { get; }

        IQueryable<Room> Rooms { get; }

        IQueryable<Group> Groups { get; }

        IQueryable<Student> Students { get; }
        
        IQueryable<ApplicationUser> Users { get; }
        
        IQueryable<Role> Roles { get; }
    }
}