using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorshopBase.Models;
using WorshopBase.ViewModels.OwnersViewModels;
using WorshopBase.ViewModels.CarsViewModels;
using WorshopBase.ViewModels;

namespace WorshopBase.Controllers
{
    [Authorize]
    public class OwnersController : Controller
    {
        WorkshopContext db;

        public OwnersController(WorkshopContext context)
        {
            db = context;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            List<OwnerViewModel> list = new List<OwnerViewModel>();
            var owners = db.Owners;
            foreach (var owner in owners)
            {
                list.Add(new OwnerViewModel
                {
                    Id = owner.ownerID,
                    driverLicense = owner.driverLicense,
                    fioOwner = owner.fioOwner,
                    adress = owner.adress,
                    phone = owner.phone
                });
            }
            IQueryable<OwnerViewModel> filterList = list.AsQueryable();
            var count = filterList.Count();
            var items = filterList.Skip((page - 1) * pageSize).
                Take(pageSize).ToList();
            OwnersListViewModel model = new OwnersListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Owners = items
            };
            return View(model);
        }

        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateOwnerViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Owners.Count(p => p.driverLicense == model.driverLicense)) == 0)
            {
                Owner owner = new Owner
                {
                    driverLicense = model.driverLicense,
                    fioOwner = model.fioOwner,
                    adress = model.adress,
                    phone = model.phone
                };
                await db.Owners.AddAsync(owner);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("driverLicense", "Запись с таким именем уже есть");
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Owner owner = await db.Owners.FirstOrDefaultAsync(t => t.ownerID == id);
                if (owner == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                    };
                    return View("Error", error);
                }
                EditOwnerViewModel model = new EditOwnerViewModel
                {
                    Id = owner.ownerID,
                    driverLicense = owner.driverLicense,
                    fioOwner = owner.fioOwner,
                    adress = owner.adress,
                    phone = owner.phone
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditOwnerViewModel model)
        {
            int er = 0;
            Owner owner = await db.Owners.FirstOrDefaultAsync(t => t.ownerID == model.Id);
            if (ModelState.IsValid && (model.driverLicense == owner.driverLicense || (er = db.Owners.Count(p => p.driverLicense == model.driverLicense)) == 0))
            {
                if (owner == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                owner.driverLicense = model.driverLicense;
                owner.fioOwner = model.fioOwner;
                owner.adress = model.adress;
                owner.phone = model.phone;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("driverLicense", "Запись с таким именем уже есть");
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Owner owner = await db.Owners.FirstOrDefaultAsync(t => t.ownerID == id);
            db.Owners.Remove(owner);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Cars(int? id)
        {
            try
            {
                if (id != null)
                {
                    Owner owner = await db.Owners.Include(x => x.Cars).ThenInclude(x => x.Owner).FirstOrDefaultAsync(t => t.ownerID == id);
                    if (owner == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                    return View(owner);
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                    return View("Error", error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение! {0} ", ex);
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateCar(int? ownerID)
        {
            try
            {
                ViewBag.ownerID = ownerID;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCar(CreateCarViewModel model)
        {
            try
            {
                Car car = new Car
                {
                    ownerID = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "ownerID").Value),
                    model = Request.Form.FirstOrDefault(p => p.Key == "model").Value,
                    vis = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "vis").Value),
                    colour = Request.Form.FirstOrDefault(p => p.Key == "colour").Value,
                    stateNumber = Request.Form.FirstOrDefault(p => p.Key == "stateNumber").Value,
                    yearOfIssue = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "yearOfIssue").Value),
                    bodyNumber = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "bodyNumber").Value),
                    engineNumber = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "engineNumber").Value)
            };
                await db.Cars.AddAsync(car);
                await db.SaveChangesAsync();
                return RedirectToAction("Cars", new { id = car.ownerID });
            }
            catch (Exception ex)
            {

            }
            ViewBag.ownerID = model.ownerID;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCar(int? id)
        {
            if (id != null)
            {
                Car car = await db.Cars.Include("Owner").FirstOrDefaultAsync(t => t.carID == id);
                if (car != null)
                {
                    EditCarViewModel model = new EditCarViewModel
                    {
                        Id = car.carID,
                        fioOwner = car.Owner.fioOwner,
                        model = car.model,
                        vis = car.vis,
                        colour = car.colour,
                        stateNumber = car.stateNumber,
                        yearOfIssue = car.yearOfIssue,
                        bodyNumber = car.bodyNumber,
                        engineNumber = car.engineNumber
                    };
                    return View(car);
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует " +
                        "запись группы с переданным id = " + id
                    };
                    return View("Error", error);
                }
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCar(Car model)
        {
            try
            {
                Car car = await db.Cars.FirstOrDefaultAsync(t => t.carID == Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "carID").Value));
                if (car == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                car.model = Request.Form.FirstOrDefault(p => p.Key == "model").Value;
                car.vis = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "vis").Value);
                car.colour = Request.Form.FirstOrDefault(p => p.Key == "colour").Value;
                car.stateNumber = Request.Form.FirstOrDefault(p => p.Key == "stateNumber").Value;
                car.yearOfIssue = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "yearOfIssue").Value);
                car.bodyNumber = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "bodyNumber").Value);
                car.engineNumber = Convert.ToInt32(Request.Form.FirstOrDefault(p => p.Key == "engineNumber").Value);
                await db.SaveChangesAsync();
                return RedirectToAction("Cars", new { id = car.ownerID });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCar(int? id)
        {
            Car car = await db.Cars.FirstOrDefaultAsync(t => t.carID == id);
            int CarId = car.ownerID;
            db.Cars.Remove(car);
            await db.SaveChangesAsync();
            return RedirectToAction("Cars", new { id = CarId });
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ErrorViewModel error = new ErrorViewModel
            {
                RequestId = "Доступ только для администратора!"
            };
            return View("Error", error);
        }
    }
}
