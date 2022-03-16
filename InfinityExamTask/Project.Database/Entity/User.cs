using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class User:EntityBase
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public List<UserExam> UserExams { get; set; }
    }
}
