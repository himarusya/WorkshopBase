using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Models
{
    public class Post
    {
        public int postID { get; set; }
        public string postName { get; set; }
        public string descriptionPost { get; set; }

        public ICollection<Worker> Workers { get; set; }
    }
}
