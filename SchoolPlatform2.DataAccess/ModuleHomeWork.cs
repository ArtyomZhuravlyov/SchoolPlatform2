using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess
{
    public class ModuleHomeWork
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Img { get; set; }

        public string? Text { get; set; }

        public string? Video { get; set; }

        public int OrderInHomeWork { get; set; }

        public HomeWork HomeWork { get; set; }

    }
}
