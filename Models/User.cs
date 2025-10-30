using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SecureSeat.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

