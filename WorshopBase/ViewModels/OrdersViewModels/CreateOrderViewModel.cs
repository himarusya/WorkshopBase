using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.OrdersViewModels
{
    public class CreateOrderViewModel
    {
        public int carID { get; set; }
        public DateTime dateReceipt { get; set; }
        public DateTime? dateCompletion { get; set; }
        public int workerID { get; set; }
    }
}
