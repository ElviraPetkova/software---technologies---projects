using BandRegister.Data;
using BandRegister.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BandRegister.Controllers
{
    public class BandController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new BandRegisterDb())
            {
                var allTeisterMask = db.Bands.ToList();
                return View(allTeisterMask);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(Band band)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            using(var db = new BandRegisterDb())
            {
                db.Bands.Add(band);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var db = new BandRegisterDb())
            {
                var bandToEdit = db.Bands.FirstOrDefault(x => x.Id == id);

                if (bandToEdit == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(bandToEdit);
            }
        }

        [HttpPost]
        public IActionResult Edit(Band band)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            using (var db = new BandRegisterDb())
            {
                var bandToEdit = db.Bands.FirstOrDefault(x => x.Id == band.Id);
                bandToEdit.Name = band.Name;
                bandToEdit.Members = band.Members;
                bandToEdit.Honorarium = band.Honorarium;
                bandToEdit.Genre = band.Genre;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var db = new BandRegisterDb())
            {
                var taskToDelete = db.Bands.FirstOrDefault(x => x.Id == id);
                if (taskToDelete == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(taskToDelete);
            }
        }

        [HttpPost]
        public IActionResult Delete(Band band)
        {
            using(var db = new BandRegisterDb())
            {
                db.Bands.Remove(band);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}