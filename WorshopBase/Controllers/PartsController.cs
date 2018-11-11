using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.ViewModels.PartsViewModels;
using WorshopBase.ViewModels;

namespace WorshopBase.Controllers
{
    [Authorize]
    public class PartsController : Controller
    {
        WorkshopContext db;

        public PartsController(WorkshopContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Parts.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePartViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Parts.Count(p => p.partName == model.partName)) == 0)
            {
                Part part = new Part
                {
                    partName = model.partName,
                    price = model.price,
                    descriptionPart = model.descriptionPart
                };
                await db.Parts.AddAsync(part);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("partName", "Запись с таким именем уже есть");
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Part part = await db.Parts.FirstOrDefaultAsync(t => t.partID == id);
                if (part == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                    };
                    return View("Error", error);
                }
                EditPartViewModel model = new EditPartViewModel
                {
                    Id = part.partID,
                    partName = part.partName,
                    price = part.price,
                    descriptionPart = part.descriptionPart
                };
                return View(model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPartViewModel model)
        {
            int er = 0;
            Part part = await db.Parts.FirstOrDefaultAsync(t => t.partID == model.Id);
            if (ModelState.IsValid && (model.partName == part.partName || (er = db.Parts.Count(p => p.partName == model.partName)) == 0))
            {
                if (part == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                part.partName = model.partName;
                part.price = model.price;
                part.descriptionPart = model.descriptionPart;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("partName", "Запись с таким именем уже есть");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Part part = await db.Parts.FirstOrDefaultAsync(t => t.partID == id);
            db.Parts.Remove(part);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}