
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPlatform2.DataAccess.UserDA;
using SchoolPlatform2.Server.Models;
using SchoolPlatform2.Server.Services;

namespace SchoolPlatform2.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task CreateUser(User user) => await _userService.CreateUser(user);

        [HttpGet]
        //[Authorize]
        public async Task<List<UserModel>> GetUsers() => await _userService.GetUsers();

        [HttpGet]
        //[Authorize]
        public async Task<UserModel> GetCurrentUser()
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (int.TryParse(userIdString, out var userId))
            {

                return await _userService.GetUser(userId);
            }
            else
                throw new Exception("you are not authorized");

        }

        [HttpPost]
        //[Authorize]
        public async Task<bool> VerifyPhone([FromBody] User user)/* => await _userService.VerifyPhone(user);*/
        {
            return  await _userService.VerifyPhone(user);
        }

        [HttpGet]
        //[Authorize]
        public async Task<List<Teacher>> GetTeachers() => await _userService.GetTeachers();

        [HttpGet]
        //[Authorize]
        public async Task<List<Student>> GetStudents() => await _userService.GetStudents();

        [HttpPost]
        public async Task CreateTeacher(Teacher teacher)
        {
            await _userService.CreateTeacher(teacher);
        }

        [HttpPost]
        public async Task UpdateTeacher(Teacher teacher)
        {
            await _userService.UpdateTeacher(teacher);
        }

        [HttpPost]
        public async Task CreateStudent(Student student)
        {
            await _userService.CreateStudent(student);
        }

        [HttpPost]
        public async Task UpdateStudent(Student student)
        {
            await _userService.UpdateStudent(student);
        }

    }
}
