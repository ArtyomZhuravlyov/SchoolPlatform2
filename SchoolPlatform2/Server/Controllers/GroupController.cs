
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SchoolPlatform2.DataAccess;
using SchoolPlatform2.Server.Models;
using SchoolPlatform2.Server.Services;
using SchoolPlatform2.Shared;
using System;

namespace SchoolPlatform2.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public GroupController(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<bool> Init()
        {
            try
            {
                _db.Roles.Add(new DataAccess.UserDA.Role() { Name = SD.ROLE_ADMINISTRATOR_IDENTITY });
                _db.Roles.Add(new DataAccess.UserDA.Role() { Name = SD.ROLE_STUDENT_IDENTITY });
                _db.Roles.Add(new DataAccess.UserDA.Role() { Name = SD.ROLE_TEACHER_IDENTITY });

                Group group = new Group();
                group.Name = "группа 1";

                Group group2 = new Group();
                group2.Name = "группа 2";

                await _db.Groups.AddAsync(group);
                await _db.Groups.AddAsync(group2);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            
            return true;
        }

        [HttpGet]
        public async Task<List<Group>> GetGroups()
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
                return await _db.Groups.AsNoTracking()
             .Include(x => x.Students).ThenInclude(x => x.User)
             .Include(x => x.Teachers).ThenInclude(x => x.User)
             .Include(x => x.Course)
             .ToListAsync();
            }
            catch(Exception ex)
            {
                return null;
            }

         
        }


        [HttpPost]
        public async Task CreateGroup(Group group)
        {
            try
            {
               
                ChangeIdCourseOnNull(group);
                group.Students = await _db.Students.Where(x => group.Students.Select(s => s.Id).Contains(x.Id)).ToListAsync();
                group.Teachers = await _db.Teachers.Where(x => group.Teachers.Select(s => s.Id).Contains(x.Id)).ToListAsync();
                await _db.Groups.AddAsync(group);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            
        }

        [HttpPost]
        public async Task UpdateGroup(Group group)
        {
            ChangeIdCourseOnNull(group);
            var gr = _db.Groups.First(x => x.Id == group.Id);
            _mapper.Map(group, gr);

            await _db.SaveChangesAsync();
        }

        private void ChangeIdCourseOnNull(Group group)
        {
            if (group.Course.Id == -1)
                group.Course = null;
        }

    }
}
