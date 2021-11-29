using DormitoryAlliance.Client.Models.Auth;
using System;
using System.Linq;

namespace DormitoryAlliance.Client.Models.Repository
{
    [Obsolete]
    public interface IDormitoryAllianceRepository
    {
        IQueryable<Dormitory> Dormitories { get; }

        IQueryable<Room> Rooms { get; }

        IQueryable<Group> Groups { get; }

        IQueryable<Student> Students { get; }

        IQueryable<Role> Roles { get; set; }

        IQueryable<User> Users { get; set; }
    }
}