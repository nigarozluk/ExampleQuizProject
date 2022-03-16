using Project.Database.Abstract;
using Project.Database.DAL;
using Project.Database.Entity;

namespace Project.Database.Concrete
{
    public class ExamQuestionRepository : RepositoryBase<ExamQuestion>, IExamQuestionRepository
    {
        public ExamQuestionRepository(ExamProjectContext Context)
           : base(Context)
        {
        }
    }
}

