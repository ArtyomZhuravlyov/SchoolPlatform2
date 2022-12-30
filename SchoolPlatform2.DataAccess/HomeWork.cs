using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess
{
    public class HomeWork
    {
        public int Id { get; set; }

        public Lesson Lesson { get; set; }

        public List<ModuleHomeWork> ModuleHomeWorks { get; set; } = new();


    }
}
