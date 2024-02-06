using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "The item's description can't be empty!")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must add your item to a category. Have you created a category yet?")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        
        public virtual ICollection<ItemTag> JoinEntities { get; set; }

        // Constructor to initialize the collection to prevent null reference exceptions
        public Item()
        {
            JoinEntities = new HashSet<ItemTag>();
        }
    }
}