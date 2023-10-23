using API.Helper;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserBs _userBs;
        IOptions<AppSettings> _options;
        public UserController(IOptions<AppSettings> options, IUserBs userBs)
        {
            _userBs = userBs;
            _options = options;
        }

        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromForm] UserDto.LoginForm form)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var mr = _userBs.Login(form, currentUserId.GetValueOrDefault()).Result;
            if (mr.Status == 200)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId,mr.Item.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken
                    (
                        issuer: _options.Value.Jwt.Issuer,
                        audience: _options.Value.Jwt.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(30),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.Key)), SecurityAlgorithms.HmacSha256)

                    );
                string tokenstr = new JwtSecurityTokenHandler().WriteToken(token);
                mr.Item.Token = tokenstr;
            }
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Post([FromForm] UserDto.Form form)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _userBs.Add(form, currentUserId.GetValueOrDefault());
            //return CreatedAtAction(nameof(SingleGet), new { id = response.Item.Id }, response);
            return await Task.FromResult(StatusCode(response.Status, response));

        }
    }
}
