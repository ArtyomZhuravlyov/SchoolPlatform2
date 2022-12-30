
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
    public class ModulehomeworkController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ModulehomeworkController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{moduleHomeWorkId}")]
        public async Task<ModuleHomeWorkDTO> GetModuleHomeWork(int? moduleHomeWorkId)
        {

            try
            {
                var mhw = await _context.ModuleHomeWorks.AsNoTracking()
                    //.Include(x => x.ModuleHomeWorks)
                    .FirstAsync(x => x.Id == moduleHomeWorkId);

                return _mapper.Map<ModuleHomeWorkDTO>(mhw);
                // .Select(x => _mapper.Map<LessonDTO>(x))
                //.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        [HttpGet]
        public async Task<List<ModuleHomeWorkDTO>> GetModuleHomeWorks()
        {
            try
            {
                return await _context.ModuleHomeWorks.AsNoTracking()
                    //.Include(x => x.ModuleHomeWorks)
                    .Select(x => _mapper.Map<ModuleHomeWorkDTO>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        [HttpPost]
        public async Task CreateModuleHomeWork(ModuleHomeWorkDTO moduleHomeWorkDTO)
        {
            try
            {
                var mhw = _mapper.Map<ModuleHomeWork>(moduleHomeWorkDTO);

                await _context.ModuleHomeWorks.AddAsync(mhw);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }

        [HttpPost]
        public async Task UpdateModuleHomeWork(ModuleHomeWorkDTO moduleHomeWorkDTO)
        {
            //ChangeIdCourseOnNull(group);
            var mhw = await _context.ModuleHomeWorks.FirstOrDefaultAsync(x => x.Id == moduleHomeWorkDTO.Id);
            _mapper.Map(moduleHomeWorkDTO, mhw);

            await _context.SaveChangesAsync();
        }

        [HttpDelete("{moduleHomeWorkId}")]
        public async Task DeleteModuleHomeWork(int? moduleHomeWorkId)
        {
            var obj = await _context.ModuleHomeWorks.FirstOrDefaultAsync(u => u.Id == moduleHomeWorkId);
            if (obj != null)
            {
                _context.ModuleHomeWorks.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }


    }
}
