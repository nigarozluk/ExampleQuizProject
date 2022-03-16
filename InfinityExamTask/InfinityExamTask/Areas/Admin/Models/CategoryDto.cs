using Project.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Models
{
    public class CategoryDto
    {
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}
