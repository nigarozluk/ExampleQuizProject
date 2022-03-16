using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class Exam:EntityBase
    {
        public string ExamName { get; set; }
        public int ExamDuration { get; set; }
        public string ExamText { get; set; }
        public int SuccessScore { get; set; }
        public List<UserExam> UserExams { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }//her sınavın bir kategorisi vardır vardır
    }
}
