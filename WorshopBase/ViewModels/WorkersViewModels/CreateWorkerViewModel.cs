using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.WorkersViewModels
{
    public class CreateWorkerViewModel
    {
        public string fioWorker { get; set; }
        public int postID { get; set; }
        public string postName { get; set; }
        public DateTime dateOfEmployment { get; set; }
        public DateTime dateOfDismissal { get; set; }
        public decimal salary { get; set; }
    }
}
