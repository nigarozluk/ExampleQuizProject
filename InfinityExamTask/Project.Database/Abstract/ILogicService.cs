
namespace Project.Database.Abstract
{
   public interface ILogicService
    {
        IUserRepository User { get; }
        ICategoryRepository Category { get; }
        IExamRepository Exam { get; }
        IQuestionRepository Question { get; }
        IOptionRepository Option { get; }
        IExamQuestionRepository ExamQuestion { get; }
        IUserExamRepository UserExam { get; }
        void Save();
    }
}
