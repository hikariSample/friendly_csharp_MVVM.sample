using MVVM.Sample.Domain.Entity;
using MVVM.Sample.Domain.Repository.Interfaces;
using MVVM.Sample.Infrastructure.Interfaces;

namespace MVVM.Sample.Repository
{
    public class StudentRepository : BaseRepository<MStudent>, IStudentRepository
    {
        public StudentRepository(IDbContext dbContext) : base(dbContext)
        { }

    }
}
