using Project.Database.Abstract;
using Project.Database.DAL;

namespace Project.Database.Concrete
{
    public class LogicService : ILogicService
    {
        private ExamProjectContext _Context;
        public LogicService(ExamProjectContext Context)
        {
            _Context = Context;
        }
        private IUserRepository _user;
        private ICategoryRepository _category;
        private IExamRepository _exam;
        private IQuestionRepository _question;
        private IOptionRepository _option;
        private IUserExamRepository _userExam;
        private IExamQuestionRepository _examQuestion;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_Context);
                }
                return _user;
            }
        }
        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_Context);
                }
                return _category;
            }
        }
        public IExamRepository Exam
        {
            get
            {
                if (_exam == null)
                {
                    _exam = new ExamRepository(_Context);
                }
                return _exam;
            }
        }
        public IQuestionRepository Question
        {
            get
            {
                if (_question == null)
                {
                    _question = new QuestionRepository(_Context);
                }
                return _question;
            }
        }
        public IOptionRepository Option
        {
            get
            {
                if (_option == null)
                {
                    _option = new OptionRepository(_Context);
                }
                return _option;
            }
        }
        public IUserExamRepository UserExam
        {
            get
            {
                if (_userExam == null)
                {
                    _userExam = new UserExamRepository(_Context);
                }
                return _userExam;
            }
        }
        public IExamQuestionRepository ExamQuestion
        {
            get
            {
                if (_examQuestion == null)
                {
                    _examQuestion = new ExamQuestionRepository(_Context);
                }
                return _examQuestion;
            }
        }
       
        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
