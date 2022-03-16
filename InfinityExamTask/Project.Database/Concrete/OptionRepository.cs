using Project.Database.Abstract;
using Project.Database.DAL;
using Project.Database.Entity;

namespace Project.Database.Concrete
{
    public class OptionRepository : RepositoryBase<Option>, IOptionRepository
    {
        public OptionRepository(ExamProjectContext Context)
           : base(Context)
        {
        }
    }
}

