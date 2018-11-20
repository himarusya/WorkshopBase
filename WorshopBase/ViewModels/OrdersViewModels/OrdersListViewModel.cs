using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.OrdersViewModels
{
    public class OrdersListViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public OrderFilterViewModel OrderFilterViewModel { get; set; }
    }
}
