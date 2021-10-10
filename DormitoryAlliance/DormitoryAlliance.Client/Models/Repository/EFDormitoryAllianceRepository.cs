using System.Linq;
using DormitoryAlliance.Client.Areas.Identity.Data;

namespace DormitoryAlliance.Client.Models.Repository
{
    public class EFDormitoryAllianceRepository : IDormitoryAllianceRepository
    {
        private DormitoryAllianceDbContext _context;

        public EFDormitoryAllianceRepository(DormitoryAllianceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Dormitory> Dormitories => _context.Dormitories;

        public IQueryable<Group> Groups => _context.Groups;

        public IQueryable<Student> Students => _context.Students;
        
        public IQueryable<ApplicationUser> Users => _context.Users;
        
        public IQueryable<Role> Roles => _context.Roles;
    }
}