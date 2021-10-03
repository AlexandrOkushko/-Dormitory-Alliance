using System.Linq;

namespace DormitoryAlliance.Client.Models.Repository
{
    public interface IDormitoryAllianceRepository
    {
        IQueryable<Dormitory> Dormitories { get; }

        IQueryable<Group> Groups { get; }

        IQueryable<Student> Students { get; }
    }
}