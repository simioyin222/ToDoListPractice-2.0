using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        // Navigation property for the relationship
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}