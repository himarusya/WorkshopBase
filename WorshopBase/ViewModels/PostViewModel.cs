using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.ViewModels
{
    public class PostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
