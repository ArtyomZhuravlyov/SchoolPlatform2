
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPlatform2.DataAccess;
using SchoolPlatform2.Server.Models;
using SchoolPlatform2.Server.Services;
using SchoolPlatform2.Shared.DTO;

namespace SchoolPlatform2.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseController(DataContext db, IMapper mapper)
        {
            _context = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CourseDTO>> GetCourses()
        {
            //_db.Roles.Add(new DataAccess.UserDA.Role() { Name = SD.ROLE_ADMINISTRATOR_IDENTITY } );
            //_db.Roles.Add(new DataAccess.UserDA.Role() { Name = SD.ROLE_STUDENT_IDENTITY });
            //_db.Roles.Add(new DataAccess.UserDA.Role() { Name = SD.ROLE_TEACHER_IDENTITY });
            //await _db.SaveChangesAsync();
            //var a = await _db.Groups
            //    .Include(x => x.Students).ThenInclude(x => x.User)
            //    .Include(x => x.Teachers)
            //    .Include(x => x.Course)
            //    .ToListAsync();

            return await _context.Courses.AsNoTracking()
                .Include(x => x.Groups)
                .Include(x => x.Lessons).ThenInclude(x => x.HomeWork).ThenInclude(x => x.ModuleHomeWorks)
                .Select(x => _mapper.Map<CourseDTO>(x))
                .ToListAsync();
        }


        [HttpGet("{courseId}")]
        public async Task<CourseDTO> GetCourse(int? courseId)
        {
            var c = await _context.Courses.AsNoTracking()
                .Include(x => x.Groups)
                .Include(x => x.Lessons).ThenInclude(x => x.HomeWork).ThenInclude(x => x.ModuleHomeWorks)
                .FirstAsync(x => x.Id == courseId);

            return _mapper.Map<CourseDTO>(c);
        }

        [HttpPost]
        public async Task CreateCourse(CourseDTO courseDTO)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDTO);

                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }

        [HttpPost]
        public async Task UpdateCourse(CourseDTO courseDTO)
        {
            //ChangeIdCourseOnNull(group);
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseDTO.Id);
            _mapper.Map(courseDTO, course);

            await _context.SaveChangesAsync();
        }

    }
}
