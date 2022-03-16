using Project.Database.Abstract;
using Project.Database.DAL;
using Project.Database.Entity;

namespace Project.Database.Concrete
{
   public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ExamProjectContext Context)
            : base(Context)
        {
        }
    }
}
