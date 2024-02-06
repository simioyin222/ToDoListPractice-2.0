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

            if (thisTag == null)
            {
                return NotFound();
            }

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
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            if (thisTag == null)
            {
                return NotFound();
            }
            return View(thisTag);
        }

        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            _db.Entry(tag).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            if (thisTag == null)
            {
                return NotFound();
            }
            return View(thisTag);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            _db.Tags.Remove(thisTag);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteJoin(int joinId)
        {
            var joinEntry = _db.ItemTags.FirstOrDefault(entry => entry.ItemTagId == joinId);
            if (joinEntry != null)
            {
                _db.ItemTags.Remove(joinEntry);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
