using Microsoft.AspNetCore.Mvc;
using Phonebook.Data;
using Phonebook.Data.Models;

namespace Phonebook.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(DataAccess.Contacts);
        }
    }
}
