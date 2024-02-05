using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;

namespace ToDoList.Controllers
{
    public class TagsController : Controller
    {
        private readonly ToDoListContext _db;

        public TagsController(ToDoListContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var tags = _db.Tags.ToList();
            return View(tags);
        }

        public ActionResult Details(int id)
        {
            var thisTag = _db.Tags
                .Include(tag => tag.JoinEntities)
                .ThenInclude(join => join.Item)
                .FirstOrDefault(tag => tag.TagId == id);
            return View(thisTag);
        }
    }
}