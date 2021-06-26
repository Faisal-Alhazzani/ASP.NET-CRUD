using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OrdersApplications.Data;
using OrdersApplications.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApplications.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CarDB carsDB = new CarDB();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCar(CarViewModel model)
        {
                string Uniquename = GenerateUniqueImageName(model.Pic.FileName);

                Task t = Task.Run(() => {
                    string PhotoPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\CarsPhoto", Uniquename);
                    var photoStream = new FileStream(PhotoPath, FileMode.Create);
                    model.Pic.CopyTo(photoStream);
                });
               
                
                Car c = new Car()
                {
                    Id = model.Id,
                    name = model.name,
                    Brand = model.Brand,
                    Price = model.Price,
                    Image = Uniquename,
                    CreationTime = model.CreationTime,
                };
                carsDB.Cars.Add(c);
                carsDB.SaveChanges();
                t.Wait();
                return RedirectToAction("ViewCars");
           
        }

        private string GenerateUniqueImageName(string fileName)
        {
            String random = Guid.NewGuid().ToString().Substring(0, 5);
            String[] nameAndExtension = fileName.Split(".");
            return nameAndExtension[0] + "_" + random + "." + nameAndExtension[1];
        }

        public IActionResult DeleteCar(string id)
        {
            Car carDel = carsDB.Cars.Where(c =>  id.Equals(c.Id)).FirstOrDefault();
            carsDB.Cars.Remove(carDel);
            carsDB.SaveChanges();
            return RedirectToAction("ViewCars");
        }

        [HttpGet]
        public IActionResult EditCar(string id)
        {
            Car FoundCar = carsDB.Cars.Find(id);
            if (FoundCar == null)
            {
                return NotFound();
            }

            
            CarViewModel model = new CarViewModel()
            {
                Id = id,
                name = FoundCar.name,
                Brand = FoundCar.Brand,
                Price = FoundCar.Price,
                CreationTime = FoundCar.CreationTime,
                Image = FoundCar.Image,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Patch(CarViewModel model, string t)
        {
            if (ModelState.IsValid) {

                Car CarToUpdate = carsDB.Cars.Find(model.Id);
                if (CarToUpdate == null)
                {
                    return NotFound();
                }
                CarToUpdate.name = model.name;
                CarToUpdate.Brand = model.Brand;
                CarToUpdate.Price = model.Price;
                CarToUpdate.CreationTime = model.CreationTime;
                carsDB.Cars.Update(CarToUpdate);
                return RedirectToAction("ViewCars");
            }
            else 
            {
                return RedirectToAction("EditCar",model.Id);
            }
        }

        [HttpGet]
        public IActionResult ViewCars()
        {
            List<Car> cars = carsDB.Cars.ToList();
            List<CarViewModel> model = cars.Select(c => new CarViewModel() {
                Id = c.Id,
                name = c.name,
                Brand = c.Brand,
                Price = c.Price,
                CreationTime = c.CreationTime,
                Image = c.Image,
            }).ToList();
            return View(model);
        }

        public IActionResult ViewCars(string s)
        {
            List<Car> cars = carsDB.Cars.Where(c => c.name.Contains(s)).ToList();
            List<CarViewModel> model = cars.Select(c => new CarViewModel()
            {
                Id = c.Id,
                name = c.name,
                Brand = c.Brand,
                Price = c.Price,
                CreationTime = c.CreationTime,
                Image = c.Image,
            }).ToList();
            return View(model);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
