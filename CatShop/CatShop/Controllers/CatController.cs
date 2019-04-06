namespace CatShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CatShop.Models;
    using System.Linq;

    public class CatController : Controller
    {
        private readonly CatDbContext context;

        public CatController(CatDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            using (var db = new CatDbContext())
            {
                var allCats = db.Cats.ToList();
                return this.View(allCats);
            }
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult Create(Cat cat)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            using (var db = new CatDbContext())
            {
                db.Cats.Add(cat);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            using (var db = new CatDbContext())
            {
                var catToEdit = db.Cats.FirstOrDefault(x => x.Id == id);
                if(catToEdit == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(catToEdit);
            }
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(Cat catModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            } 

            using (var db = new CatDbContext())
            {
                var catToEdit = db.Cats.FirstOrDefault(x => x.Id == catModel.Id);

                catToEdit.Name = catModel.Name;
                catToEdit.Nickname = catModel.Nickname;
                catToEdit.Price = catModel.Price;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            using (var db = new CatDbContext())
            {
                var catToDelete = db.Cats.FirstOrDefault(x => x.Id == id);
                if (catToDelete == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(catToDelete);
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(Cat catModel)
        {
            using (var db = new CatDbContext())
            {
                var catToDelete = db.Cats.FirstOrDefault(x => x.Id == catModel.Id);

                db.Cats.Remove(catToDelete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
