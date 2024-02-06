using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        
        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
        
        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; } = false; // Default to false for new items
        
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        // Assuming a foreign key relationship with a Category
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        // Navigation property for the related Category
        public virtual Category Category { get; set; }
        
        // For a many-to-many relationship with Tags
        public virtual ICollection<ItemTag> JoinEntities { get; set; }

        // Constructor to initialize the collection property
        public Item()
        {
            this.JoinEntities = new HashSet<ItemTag>();
        }
    }
}