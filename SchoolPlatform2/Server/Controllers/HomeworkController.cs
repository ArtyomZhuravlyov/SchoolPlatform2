
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
    public class HomeworkController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HomeworkController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{homeWorkId}")]
        public async Task<HomeWorkDTO> GetHomeWork(int? homeWorkId)
        {

            try
            {
                var hm = await _context.HomeWorks.AsNoTracking()
                    .Include(x => x.ModuleHomeWorks)
                    .FirstAsync(x => x.Id == homeWorkId);

                return _mapper.Map<HomeWorkDTO>(hm);
                // .Select(x => _mapper.Map<LessonDTO>(x))
                //.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        [HttpGet]
        public async Task<List<HomeWorkDTO>> GetHomeWorks()
        {
            try
            {
                return await _context.HomeWorks.AsNoTracking()
                    .Include(x => x.ModuleHomeWorks)
                    .Select(x => _mapper.Map<HomeWorkDTO>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        [HttpPost]
        public async Task CreateHomeWork(HomeWorkDTO homeWorkDTO)
        {
            try
            {
                var hm = _mapper.Map<HomeWork>(homeWorkDTO);

                await _context.HomeWorks.AddAsync(hm);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }

        [HttpPost]
        public async Task UpdateHomeWork(HomeWorkDTO homeWorkDTO)
        {
            //ChangeIdCourseOnNull(group);
            var hm = await _context.HomeWorks.FirstOrDefaultAsync(x => x.Id == homeWorkDTO.Id);
            _mapper.Map(homeWorkDTO, hm);

            await _context.SaveChangesAsync();
        }

        [HttpDelete("{homeWorkId}")]
        public async Task DeleteHomeWork(int? homeWorkId)
        {
            var obj = await _context.HomeWorks.FirstOrDefaultAsync(u => u.Id == homeWorkId);
            if (obj != null)
            {
                _context.HomeWorks.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }


    }
}
