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
    public class WorkersController : Controller
    {
        WorkshopContext db;

        public WorkersController(WorkshopContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            var workersList = db.Workers.Include("Post").ToList();
            List<WorkerViewModel> list = new List<WorkerViewModel>();
            foreach (var item in workersList)
            {
                list.Add(new WorkerViewModel
                {
                    Id = item.workerID,
                    fioWorker = item.fioWorker,
                    postName = item.Post.postName,
                    dateOfEmployment = item.dateOfEmployment,
                    dateOfDismissal = item.dateOfDismissal,
                    salary = item.salary
                });
            }
            return View(list);
        }
    }
}
