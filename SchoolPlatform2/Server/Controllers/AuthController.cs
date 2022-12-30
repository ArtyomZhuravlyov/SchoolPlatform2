
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPlatform2.Server.Models;
using SchoolPlatform2.Server.Services;

namespace SchoolPlatform2.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<TokenModel> Token(TokenRequestModel model)
            => await _userService.GetToken(model.Login, model.Pass);

        [HttpPost]
        public async Task<TokenModel> RefreshToken(RefreshTokenRequestModel model)
            => await _userService.GetTokenByRefreshToken(model.RefreshToken);
    }
}
