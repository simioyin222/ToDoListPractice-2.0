using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Models;
using System.Linq;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ToDoListContext _db;

        public ItemsController(ToDoListContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var items = _db.Items.Include(item => item.JoinEntities).ThenInclude(join => join.Tag).ToList();
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var thisItem = _db.Items
                              .Include(item => item.JoinEntities)
                              .ThenInclude(join => join.Tag)
                              .FirstOrDefault(item => item.ItemId == id);
            if (thisItem == null)
            {
                return NotFound();
            }
            return View(thisItem);
        }

        public IActionResult Create()
        {
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item, int[] tagIds)
        {
            _db.Items.Add(item);
            _db.SaveChanges();

            if (tagIds.Length > 0)
            {
                foreach (int tagId in tagIds)
                {
                    _db.ItemTags.Add(new ItemTag { ItemId = item.ItemId, TagId = tagId });
                }
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            if (thisItem == null)
            {
                return NotFound();
            }
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item, int[] tagIds)
        {
            if (ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();

                var existingJoins = _db.ItemTags.Where(join => join.ItemId == item.ItemId).ToList();
                foreach (var join in existingJoins)
                {
                    _db.ItemTags.Remove(join);
                }
                _db.SaveChanges();

                if (tagIds.Length > 0)
                {
                    foreach (int tagId in tagIds)
                    {
                        _db.ItemTags.Add(new ItemTag { ItemId = item.ItemId, TagId = tagId });
                    }
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            if (thisItem == null)
            {
                return NotFound();
            }
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            _db.Items.Remove(thisItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddTag(int id)
        {
            var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult AddTag(Item item, int tagId)
        {
            if (tagId != 0)
            {
                _db.ItemTags.Add(new ItemTag { TagId = tagId, ItemId = item.ItemId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = item.ItemId });
        }
    }
}