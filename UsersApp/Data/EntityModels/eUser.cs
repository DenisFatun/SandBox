using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UsersApp.Data.EntityModels
{
    [Index(nameof(Login), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class eUser
    {
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string Login { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string Email { get; set; }

        public DateOnly BirthDate { get; set; }
        
        public bool IsMale { get; set; }

        [Required, MinLength(3), MaxLength(512)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? LastAttempt { get; set; }

        public int Attempts { get; set; }
    }
}
