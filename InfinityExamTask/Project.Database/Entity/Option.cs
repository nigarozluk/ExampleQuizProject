using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class Option:EntityBase
    {
        public string OptionText { get; set; }
        public bool IsTrue { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }//her şık kaydının bir sorusu vardır vardır
    }
}
