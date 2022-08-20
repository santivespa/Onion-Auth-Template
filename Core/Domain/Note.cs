using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Note
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
    }
}
