using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.ViewModels.UsersViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
