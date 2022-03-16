using Project.Database.Abstract;
using Project.Database.DAL;
using Project.Database.Entity;

namespace Project.Database.Concrete
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ExamProjectContext Context)
           : base(Context)
        {
        }
    }
}

