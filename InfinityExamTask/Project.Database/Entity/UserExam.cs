using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class UserExam:EntityBase
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int ExamID { get; set; }
        public Exam Exam { get; set; }
        public int UserScore { get; set; }
        public bool IsStarted { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TakingExamDate { get; set; }

    }
}
