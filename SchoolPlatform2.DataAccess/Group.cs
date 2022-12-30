
using SchoolPlatform2.DataAccess.UserDA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess
{
    public class Group
    {
        //[Required]
        ////[RegularExpression(@".*[^\-][^1].*")]
        //[Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Course? Course { get; set; }

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();

        public List<Student> Students { get; set; } = new List<Student>();


    }
}
