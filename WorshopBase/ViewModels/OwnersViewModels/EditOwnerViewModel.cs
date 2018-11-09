using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.OwnersViewModels
{
    public class EditOwnerViewModel
    {
        public int Id { get; set; }
        public int driverLicense { get; set; }
        public string fioOwner { get; set; }
        public string adress { get; set; }
        public int phone { get; set; }
    }
}
