using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.UsersViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
