using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Database.Entity
{
   public class EntityBase
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
