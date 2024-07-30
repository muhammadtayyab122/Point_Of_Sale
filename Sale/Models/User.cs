using System.ComponentModel.DataAnnotations;

namespace Sale.Model
{
    public class User
    {
        public int Id { get; set; }

       
        [MaxLength(50)]
        public string FirstName { get; set; }

    
        [MaxLength(50)]
        public string LastName { get; set; }

       
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]  // Assuming you store a hashed password
        public string Password { get; set; }
    }
}
