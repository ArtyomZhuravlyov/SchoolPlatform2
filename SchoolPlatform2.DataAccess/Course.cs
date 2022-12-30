using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess
{
    public class Course
    {
        //[Range(1, int.MaxValue)]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Test { get; set; }

        public string? Description { get; set; }

        public List<Group> Groups { get; set; } = new();

        public List<Lesson> Lessons { get; set; } = new();
    }
}
