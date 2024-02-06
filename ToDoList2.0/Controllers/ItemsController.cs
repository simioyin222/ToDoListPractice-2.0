using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ToDoListContext _db;

        public ItemsController(ToDoListContext db)
        {
            _db = db;
        }

        // GET: Items
        public IActionResult Index()
        {
            var items = _db.Items.Include(i => i.Category).ToList();
            return View(items);
        }

        // GET: Items/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _db.Items
                .Include(i => i.Category)
                .FirstOrDefault(m => m.ItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", item.CategoryId);
                return View(item);
            }
            
            _db.Items.Add(item);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Items/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(item);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _db.Items
                .Include(i => i.Category)
                .FirstOrDefault(m => m.ItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}