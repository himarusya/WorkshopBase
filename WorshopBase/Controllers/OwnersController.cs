using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.ViewModels;

namespace WorshopBase.Controllers
{
    public class OwnersController : Controller
    {
        WorkshopContext db;

        public OwnersController(WorkshopContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var ownersList = db.Owners.ToList();
            List<OwnerViewModel> list = new List<OwnerViewModel>();
            foreach (var item in ownersList)
            {
                list.Add(new OwnerViewModel
                {
                    Id = item.ownerID,
                    driverLicense = item.driverLicense,
                    fioOwner = item.fioOwner,
                    adress = item.adress,
                    phone = item.phone
                });
            }
            return View(list);
        }
    }
}
