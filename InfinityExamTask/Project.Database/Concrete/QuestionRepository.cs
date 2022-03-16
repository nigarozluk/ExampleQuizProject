using Project.Database.Abstract;
using Project.Database.DAL;
using Project.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Concrete
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(ExamProjectContext Context)
           : base(Context)
        {
        }
    }
}
