using DormitoryAlliance.Client.Models.Auth;
using System;
using System.Linq;

namespace DormitoryAlliance.Client.Models.Repository
{
    [Obsolete]
    public class EFDormitoryAllianceRepository : IDormitoryAllianceRepository
    {
        private DormitoryAllianceDbContext _context;

        public EFDormitoryAllianceRepository(DormitoryAllianceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Dormitory> Dormitories => _context.Dormitories;

        public IQueryable<Room> Rooms => _context.Rooms;

        public IQueryable<Group> Groups => _context.Groups;

        public IQueryable<Student> Students => _context.Students;

        public IQueryable<Role> Roles
        {
            get => _context.Roles;
            set
            {
                if (value is not null)
                {
                    _context.Roles.AddRange(value); //todo fix
                }
            }
        }

        public IQueryable<User> Users
        {
            get => _context.Users;
            set
            {
                if (value is not null)
                {
                    _context.Users.AddRange(value);
                }
            }
        }

    }
}