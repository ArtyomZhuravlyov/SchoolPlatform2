
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolPlatform2.DataAccess;
using SchoolPlatform2.DataAccess.UserDA;
using SchoolPlatform2.Server.Configs;
using SchoolPlatform2.Server.Models;
using SchoolPlatform2.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolPlatform2.Server.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly AuthConfig _config;

        public UserService(IMapper mapper, DataContext context, IOptions<AuthConfig> config)
        {
            _mapper = mapper;
            _context = context;
            _config = config.Value;
        }

        public async Task CreateUser(User user)
        {
            /*var dbUser = _mapper.Map<DataAccess.UserDA.User>(model);*/
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }


        public async Task CreateTeacher(Teacher teacher)
        {
            teacher.User.PasswordHash = HashHelper.GetHash(teacher.User.PasswordHash);
            teacher.User.Roles.Add(_context.Roles.First(x => x.Name == SD.ROLE_STUDENT_IDENTITY));
            teacher.Groups = await _context.Groups.Where(x => teacher.Groups.Select(g => g.Id).Contains(x.Id)).ToListAsync();
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            var th = _context.Teachers.First(x => x.Id == teacher.Id);
            if (teacher.User.PasswordHash != "")
                teacher.User.PasswordHash = HashHelper.GetHash(teacher.User.PasswordHash);

            //student.Group = _context.Groups.First(x => x.Id == student.Group.Id);
            _mapper.Map(teacher, th);

            await _context.SaveChangesAsync();
        }

        public async Task CreateStudent(Student student)
        {
            student.User.PasswordHash = HashHelper.GetHash(student.User.PasswordHash);
            student.User.Roles.Add(_context.Roles.First(x => x.Name == SD.ROLE_STUDENT_IDENTITY));
            student.Groups = await _context.Groups.Where(x => student.Groups.Select(g => g.Id).Contains(x.Id)).ToListAsync();

            //student.Group = _context.Groups.First(x => x.Id == student.Group.Id);
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            var st = _context.Students.First(x => x.Id == student.Id);
            if (student.User.PasswordHash != "")
                student.User.PasswordHash = HashHelper.GetHash(student.User.PasswordHash);

            //student.Group = _context.Groups.First(x => x.Id == student.Group.Id);
            _mapper.Map(student, st);

            await _context.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetUsers()
        {
            return await _context.Users.AsNoTracking().ProjectTo<UserModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            return await _context.Teachers.AsNoTracking()
                .Include(x => x.Groups)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<List<Student>> GetStudents()
        {
            //var ff = await _context.Students.AsNoTracking()
            //    .Include(x => x.Groups)
            //    .Include(x => x.LessonTest)
            //    .ToListAsync();

            return await _context.Students.AsNoTracking()
                .Include(x => x.Groups)
                 .Include(x => x.User)
                .Include(x => x.LessonTest)
                .ToListAsync();
        }

        private async Task<DataAccess.UserDA.User> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new Exception("user not found");
            return user;
        }

        public async Task<UserModel> GetUser(int id)
        {
            var user = await GetUserById(id);

            return _mapper.Map<UserModel>(user);

        }

        public async Task<bool> VerifyPhone(User user) => !_context.Users.Any(x => x.Phone == user.Phone);

        private async Task<DataAccess.UserDA.User> GetUserByCredention(string login, string pass)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == login.ToLower());
            if (user == null)
                throw new Exception("user not found");

            if (!HashHelper.Verify(pass, user.PasswordHash))
                throw new Exception("password is incorrect");

            return user;
        }

        private TokenModel GenerateTokens(DataAccess.UserDA.User user)
        {
            var dtNow = DateTime.Now;

            var jwt = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                notBefore: dtNow,
                claims: new Claim[] {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            new Claim("id", user.Id.ToString()),
            },
                expires: DateTime.Now.AddMinutes(_config.LifeTime),
                signingCredentials: new SigningCredentials(_config.SymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refresh = new JwtSecurityToken(
                notBefore: dtNow,
                claims: new Claim[] {
                new Claim("id", user.Id.ToString()),
                },
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: new SigningCredentials(_config.SymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedRefresh = new JwtSecurityTokenHandler().WriteToken(refresh);

            return new TokenModel(encodedJwt, encodedRefresh);

        }
        public async Task<TokenModel> GetToken(string login, string password)
        {
            var user = await GetUserByCredention(login, password);

            return GenerateTokens(user);
        }

        public async Task<TokenModel> GetTokenByRefreshToken(string refreshToken)
        {
            var validParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = _config.SymmetricSecurityKey()
            };
            var principal = new JwtSecurityTokenHandler().ValidateToken(refreshToken, validParams, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtToken
                || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("invalid token");
            }

            if (principal.Claims.FirstOrDefault(x => x.Type == "id")?.Value is String userIdString
                && Int32.TryParse(userIdString, out var userId))
            {
                var user = await GetUserById(userId);
                return GenerateTokens(user);
            }
            else
            {
                throw new SecurityTokenException("invalid token");
            }
        }

    }
}
