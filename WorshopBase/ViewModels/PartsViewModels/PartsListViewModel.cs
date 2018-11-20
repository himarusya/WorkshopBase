using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.PartsViewModels
{
    public class PartsListViewModel
    {
        public IEnumerable<PartViewModel> Parts { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
