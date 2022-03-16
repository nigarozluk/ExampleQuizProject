using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class Category:EntityBase
    {
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
        public List<Exam> Exams { get; set; }//Her KATEGORİNİN birden fazla SINAVI olabilir
        public List<Question> Questions { get; set; }//Her KATEGORİNİN birden fazla sorusu olabilir
    }
    public enum CategoryType : byte
    {
        [Description("Exam")]
        Exam = 0,
        [Description("Question")]
        Question = 1,

    }
}
