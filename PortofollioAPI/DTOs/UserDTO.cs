using System.ComponentModel.DataAnnotations;

namespace PortofollioAPI.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime? DOB { get; set; }
    }
}
