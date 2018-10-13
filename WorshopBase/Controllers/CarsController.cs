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
    public class CarsController : Controller
    {
        WorkshopContext db;

        public CarsController(WorkshopContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var carsList = db.Cars.Include("Owner").ToList();
            List<CarViewModel> list = new List<CarViewModel>();
            foreach (var item in carsList)
            {
                list.Add(new CarViewModel
                {
                    Id = item.carID,
                    fioOwner = item.Owner.fioOwner,
                    model = item.model,
                    vis = item.vis,
                    colour = item.colour,
                    stateNumber = item.stateNumber,
                    yearOfIssue = item.yearOfIssue,
                    bodyNumber = item.bodyNumber,
                    engineNumber = item.engineNumber
                });
            }
            return View(list);
        }
    }
}
