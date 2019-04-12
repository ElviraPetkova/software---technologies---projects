namespace ProjectRider.App.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class ProjectController : Controller
    {
        private readonly ProjectRiderDbContext context;

        public ProjectController(ProjectRiderDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            using (var db = new ProjectRiderDbContext())
            {
                var allProject = db.Projects.ToList();
                return this.View(allProject);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Index");
            //}

            using (var db = new ProjectRiderDbContext())
            {
                db.Projects.Add(project);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            using (var db = new ProjectRiderDbContext())
            {
                var projectToEdit = db.Projects.FirstOrDefault(p => p.Id == id);
                if(projectToEdit == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(projectToEdit);
            }
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int id, Project projectModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Index");
            //}

            using (var db = new ProjectRiderDbContext())
            {
                var projectToEdit = db.Projects.FirstOrDefault(x => x.Id == id);

                projectToEdit.Title = projectModel.Title;
                projectToEdit.Description = projectModel.Description;
                projectToEdit.Budget = projectModel.Budget;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            using (var db = new ProjectRiderDbContext())
            {
                var projectToDelete = db.Projects.FirstOrDefault(p => p.Id == id);
                if (projectToDelete == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(projectToDelete);
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id, Project projectModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Index");
            //}

            using (var db = new ProjectRiderDbContext())
            {
                var projectToDelete = db.Projects.FirstOrDefault(x => x.Id == id);

                db.Projects.Remove(projectToDelete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}