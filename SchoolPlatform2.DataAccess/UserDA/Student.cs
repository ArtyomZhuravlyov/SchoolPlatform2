using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess.UserDA
{
    public class Student
    {
        public int Id { get; set; }

        public int Coin { get; set; }

        [Required]
        public string PhoneParent { get; set; }

        public int UserId { get; set; }

        [ValidateComplexType]
        public User User { get; set; }

        public Lesson? LessonTest { get; set; }

        public List<Group> Groups { get; set; } = new List<Group>();

    }
}
