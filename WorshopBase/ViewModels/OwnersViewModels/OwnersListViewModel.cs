using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.OwnersViewModels
{
    public class OwnersListViewModel
    {
        public IEnumerable<OwnerViewModel> Owners { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
