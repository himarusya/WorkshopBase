using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.PartsViewModels
{
    public class CreatePartViewModel
    {
        public string partName { get; set; }
        public decimal price { get; set; }
        public string descriptionPart { get; set; }
    }
}
