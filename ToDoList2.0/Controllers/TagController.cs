using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            return View(_db.Tags.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisTag = _db.Tags
                .Include(tag => tag.JoinEntities)
                .ThenInclude(join => join.Item)
                .FirstOrDefault(tag => tag.TagId == id);
            return View(thisTag);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddItem(int id)
        {
            var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Description");
            return View(thisTag);
        }

        [HttpPost]
        public IActionResult AddItem(Tag tag, int itemId)
        {
            if (!_db.ItemTags.Any(join => join.ItemId == itemId && join.TagId == tag.TagId))
            {
                _db.ItemTags.Add(new ItemTag { ItemId = itemId, TagId = tag.TagId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = tag.TagId });
        }

        // Additional actions for Edit, Delete, etc., can be added here as needed.
    }
}