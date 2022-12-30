using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess.UserDA
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
