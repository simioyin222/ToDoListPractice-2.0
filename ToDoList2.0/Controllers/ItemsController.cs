using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var items = _db.Items
                .Include(item => item.Category) // Load each item's Category.
                .ToList();
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var thisItem = _db.Items
                .Include(item => item.Category) // Include the Category of the item.
                .Include(item => item.JoinEntities) // Include ItemTag join entities.
                .ThenInclude(join => join.Tag) // For each join entity, include the Tag.
                .FirstOrDefault(item => item.ItemId == id); // Find the item by ID.

            if (thisItem == null)
            {
                return NotFound();
            }

            return View(thisItem);
        }
    }
}