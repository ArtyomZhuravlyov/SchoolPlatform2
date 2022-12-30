using System.ComponentModel.DataAnnotations;

namespace SchoolPlatform2.DataAccess.UserDA
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Будет логином
        /// </summary>
        [Required]
        public string Phone { get; set; }
        [Required]
        public string PasswordHash { get; set; } 

        public string ImgAvatar { get; set; } 

        public DateTimeOffset BirthDate { get; set; }

        public List<Role> Roles { get; set; } = new();

       
        public Student? Student { get; set; }

        public Teacher? Teacher { get; set; }
    }
}