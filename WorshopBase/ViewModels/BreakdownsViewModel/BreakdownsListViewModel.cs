using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.BreakdownsViewModel
{
    public class BreakdownsListViewModel
    {
        public IEnumerable<BreakdownViewModel> Breakdowns { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
