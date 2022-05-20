using System.ComponentModel.DataAnnotations;

namespace crud.DTOs
{
    public class EmployeeDto
    {   
        /// <summary>
        /// Id required only in search opertion, when provided on create or update
        /// the value will be ignored
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name requires at least 1 letter
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "Name must be at least 1 char long.")]
        public string Name { get; set; }

        /// <summary>
        /// Age is required. Valid range from 18 to 200. Are older than 200?
        /// </summary>
        [Required]
        [Range(18, 200, ErrorMessage = "Age must be in a range 18-200.")]
        public int Age { get; set; }
    }
}
