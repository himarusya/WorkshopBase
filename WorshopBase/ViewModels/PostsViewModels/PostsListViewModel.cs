using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.PostsViewModels
{
    public class PostsListViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
