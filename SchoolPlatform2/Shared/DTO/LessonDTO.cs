using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.Shared.DTO
{
    public class LessonDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        //public bool TestLesson { get; set; }

        //[Required]
        public CourseDTO Course { get; set; }

        public int? HomeWorkId { get; set; }
        public HomeWorkDTO? HomeWork { get; set; }
    }
}
