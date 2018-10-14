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
    public class PartsController : Controller
    {
        WorkshopContext db;

        public PartsController(WorkshopContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            var partsList = db.Parts.ToList();
            List<PartViewModel> list = new List<PartViewModel>();
            foreach (var item in partsList)
            {
                list.Add(new PartViewModel
                {
                    Id = item.partID,
                    partName = item.partName,
                    price = item.price,
                    descriptionPart = item.descriptionPart
                });
            }
            return View(list);
        }
    }
}