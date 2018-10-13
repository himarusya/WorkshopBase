using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string postName { get; set; }
        public string descriptionPost { get; set; }
    }
}
