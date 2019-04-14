using RescueRegister.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RescueRegister.Controllers
{
    public class MountaineerController : Controller
    {
        private readonly RescueRegisterDbContext context;

        public MountaineerController(RescueRegisterDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            using (var db = new RescueRegisterDbContext())
            {
                var allMountaineers = db.Mountaineers.ToList();
                return this.View(allMountaineers);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(Mountaineer mountaineer)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            using (var db = new RescueRegisterDbContext())
            {
                db.Mountaineers.Add(mountaineer);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var db = new RescueRegisterDbContext())
            {
                var mountaineerToEdit = db.Mountaineers.FirstOrDefault(x => x.Id == id);
                if (mountaineerToEdit == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(mountaineerToEdit);
            }
        }

        [HttpPost]
        public IActionResult Edit(Mountaineer mountaineer)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            using (var db = new RescueRegisterDbContext())
            {
                var mountaineerToEdit = db.Mountaineers.FirstOrDefault(x => x.Id == mountaineer.Id);

                mountaineerToEdit.Name = mountaineer.Name;
                mountaineerToEdit.Age = mountaineer.Age;
                mountaineerToEdit.Gender = mountaineer.Gender;
                mountaineerToEdit.LastSeenDate = mountaineer.LastSeenDate;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var db = new RescueRegisterDbContext())
            {
                var mountaineerToDelete = db.Mountaineers.FirstOrDefault(x => x.Id == id);
                if (mountaineerToDelete == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(mountaineerToDelete);
            }
        }

        [HttpPost]
        public IActionResult Delete(Mountaineer mountaineer)
        {
            using (var db = new RescueRegisterDbContext())
            {
                var mountaineerToDelete = db.Mountaineers.FirstOrDefault(x => x.Id == mountaineer.Id);

                db.Mountaineers.Remove(mountaineerToDelete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}