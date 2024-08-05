using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    public class ToDoItemRequest
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool Completed { get; set; }
    }
}
