using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolPlatform2.DataAccess.UserDA;

namespace SchoolPlatform2.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Shedule> Shudeles { get; set; }

        public DbSet<HomeWork> HomeWorks { get; set; }

        public DbSet<ModuleHomeWork> ModuleHomeWorks { get; set; }

        public DbSet<Role> Roles { get; set; }

    }
}
