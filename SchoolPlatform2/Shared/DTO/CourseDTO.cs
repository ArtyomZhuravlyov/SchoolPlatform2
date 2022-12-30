using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.Shared.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        //public List<Group> Groups { get; set; } = new();

        public List<LessonDTO> Lessons { get; set; } = new();
    }
}
