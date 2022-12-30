using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int OrderInCourse { get; set; }

        //public bool TestLesson { get; set; }

        public Course Course { get; set; }

        public int? HomeWorkId { get; set; }
        public HomeWork? HomeWork { get; set; }
    }
}
