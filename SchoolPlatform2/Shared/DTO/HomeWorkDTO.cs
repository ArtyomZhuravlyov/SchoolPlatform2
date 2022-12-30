using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.Shared.DTO
{
    public class HomeWorkDTO
    {
        public int? Id { get; set; }

        //[Required]
        //public LessonDTO Lesson { get; set; }

        public List<ModuleHomeWorkDTO> ModuleHomeWorks { get; set; } = new();
    }
}
