using Project.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Models
{
    public class QuestionDto
    {
        public int QueID { get; set; }
        public string QuestionText { get; set; }
        public int? CategoryID { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Answer { get; set; }
        public List<string> listOfOptions { get; set; }
        public string selectedValue { get; set; }
    }
}
