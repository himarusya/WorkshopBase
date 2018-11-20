using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.ViewModels.BreakdownsViewModel
{
    public class BreakdownViewModel
    {
        public int Id { get; set; }
        public string partName { get; set; }
        public string fioWorker { get; set; }
        public string stateNumber { get; set; }
        public decimal price { get; set; }
    }
}
