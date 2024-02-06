using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime DueDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ItemTag> JoinEntities { get; set; }
    }
}