using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.Shared.DTO
{
    public class ModuleHomeWorkDTO
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Img { get; set; }

        public string? Text { get; set; }

        public string? Video { get; set; }

    }
}
