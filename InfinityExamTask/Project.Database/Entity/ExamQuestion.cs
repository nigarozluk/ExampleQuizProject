using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class ExamQuestion:EntityBase
    {
        public int ExamID { get; set; }
        public Exam Exam { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
    }
}
