using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.BreakdownsViewModel
{
    public class EditBreakdownViewModel
    {
        public int Id { get; set; }
        public int partID { get; set; }
        public int workerID { get; set; }
        public int orderID { get; set; }
    }
}
