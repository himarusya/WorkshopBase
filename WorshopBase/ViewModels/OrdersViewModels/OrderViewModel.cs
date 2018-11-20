using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.ViewModels.OrdersViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string stateNumber { get; set; }
        public string fioOwner { get; set; }
        public DateTime dateReceipt { get; set; }
        public DateTime? dateCompletion { get; set; }
        public string fioWorker { get; set; }
        public int workerID { get; set; }
        public decimal price { get; set; }
    }
}
