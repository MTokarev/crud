using System.ComponentModel.DataAnnotations;

namespace crud.DTOs
{
    public class EmployeeDto
    {   
        public int Id { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "Name must be at least 1 char long.")]
        public string Name { get; set; }

        [Required]
        [Range(18, 200, ErrorMessage = "Age must be in a range 18-200.")]
        public int Age { get; set; }
    }
}
