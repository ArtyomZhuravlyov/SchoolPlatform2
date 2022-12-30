
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPlatform2.DataAccess;
using SchoolPlatform2.DataAccess.UserDA;
using SchoolPlatform2.Server.Models;
using SchoolPlatform2.Server.Services;
using SchoolPlatform2.Shared.DTO;

namespace SchoolPlatform2.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LessonController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LessonController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{lessonId}")]
        public async Task<LessonDTO> GetLesson(int? lessonId)
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
            try
            {
                var les = await _context.Lessons.AsNoTracking()
                    .Include(x => x.Course)
                    .FirstAsync(x => x.Id == lessonId);

                return _mapper.Map<LessonDTO>(les);
                // .Select(x => _mapper.Map<LessonDTO>(x))
                //.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        [HttpGet]
        public async Task<List<LessonDTO>> GetLessons()
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
            try
            {
                return await _context.Lessons.AsNoTracking()
                    .Include(x => x.HomeWork)
                    .Include(x => x.Course)
                    .Select(x => _mapper.Map<LessonDTO>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        [HttpPost]
        public async Task CreateLesson(LessonDTO lessonDTO)
        {
            try
            {
                var lesson = _mapper.Map<Lesson>(lessonDTO);

                await _context.Lessons.AddAsync(lesson);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }

        [HttpPost]
        public async Task UpdateLesson(LessonDTO lessonDTO)
        {
            //ChangeIdCourseOnNull(group);
            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == lessonDTO.Id);
            _mapper.Map(lessonDTO, lesson);

            await _context.SaveChangesAsync();
        }

        [HttpDelete("{lessonId}")]
        public async Task DeleteLesson(int? lessonId)
        {
            var obj = await _context.Lessons.FirstOrDefaultAsync(u => u.Id == lessonId);
            if (obj != null)
            {
                _context.Lessons.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }


    }
}
