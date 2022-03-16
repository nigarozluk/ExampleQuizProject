using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Models
{
    public class ScoreListDto
    {
        public string UserMail { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate{ get; set; }
        public int UserScore { get; set; }

    }
}
