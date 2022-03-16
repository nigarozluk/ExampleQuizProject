using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class Question:EntityBase
    {
        public string QuestionText { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }//her sorunun bir kategorisi vardır vardır
        public QuestionType QuestionType { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
        public List<Option> Options { get; set; }//Her sorunun birden fazla şıkkı olabilir
    }
    public enum QuestionType : byte
    {
        [Description("WithoutOption")]
        WithoutOption = 0,
        [Description("WithOptions")]
        WithOptions = 1,
       
    }
}
