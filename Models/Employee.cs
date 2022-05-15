using System.ComponentModel.DataAnnotations;

namespace crud.Models
{
    /// <summary>
    /// Represent Employee model
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Employee name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Employee age
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// Time in UTC when Employee was created
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Time in UTC when Employee was updated
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

    }
}
