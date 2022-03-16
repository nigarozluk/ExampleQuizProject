using Project.Database.Abstract;
using Project.Database.DAL;
using Project.Database.Entity;

namespace Project.Database.Concrete
{
    public class UserExamRepository : RepositoryBase<UserExam>, IUserExamRepository
    {
        public UserExamRepository(ExamProjectContext Context)
           : base(Context)
        {
        }
    }
}

