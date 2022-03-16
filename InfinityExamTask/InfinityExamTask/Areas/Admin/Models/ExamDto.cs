using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Models
{
    public class ExamDto
    {
        public string ExamName { get; set; }
        public int ExamDuration { get; set; }
        public string ExamText { get; set; }
        public int SuccessScore { get; set; }
        public int? CategoryID { get; set; }
    }
}
