using Microsoft.EntityFrameworkCore;
using Project.Database.Entity;

namespace Project.Database.DAL
{
   public class ExamProjectContext: DbContext
    {
        public ExamProjectContext(DbContextOptions<ExamProjectContext> options)
         : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<UserExam> UserExams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamQuestion>().HasKey(pk => new { pk.ExamID, pk.QuestionID });
            modelBuilder.Entity<UserExam>().HasKey(pk => new { pk.ExamID, pk.UserID });
        }
    }
}
