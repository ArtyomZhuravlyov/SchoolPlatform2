using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPlatform2.DataAccess.UserDA
{
    public class Teacher
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Group> Groups { get; set; } = new();
    }
}
