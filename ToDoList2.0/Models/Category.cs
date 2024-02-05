using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Navigation property for the relationship
        public List<Item> Items { get; set; }
    }
}