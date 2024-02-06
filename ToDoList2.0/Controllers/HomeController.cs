using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoListContext _db;

        public HomeController(ToDoListContext db)
        {
            _db = db;
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            var categories = _db.Categories.ToList();
            var items = _db.Items.ToList();
            var model = new Dictionary<string, object>
            {
                { "categories", categories },
                { "items", items }
            };
            return View(model);
        }
    }
}