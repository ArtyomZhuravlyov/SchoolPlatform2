using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess
{
    public class Shedule
    {
        public int Id { get; set; }

        public DateTimeOffset StartLesson { get; set; }

        public List<Group> Groups { get;} = new();

        public List<Lesson> Lessons { get; } = new();
    }
}
