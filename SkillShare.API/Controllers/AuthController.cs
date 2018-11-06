using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkillShare.API.Data;
using SkillShare.API.Models;
using SkillShare.API.Dtos;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SkillShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository repo;

       private readonly IConfiguration Config;

        public AuthController(IAuthRepository repo,IConfiguration config )
        {
            this.repo = repo;
            Config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto){
                //validate request

               userForRegisterDto.Username=userForRegisterDto.Username.ToLower();
                if(await repo.UserExists(userForRegisterDto.Username)){
                    return BadRequest("User name already exists!");
                }
                var userToCreate=new User
                {
                    Username=userForRegisterDto.Username
                };

                var createdUser=await repo.Register(userToCreate,userForRegisterDto.Password);
                return StatusCode(201);
        }

        [HttpPost("login")]
       public async Task<IActionResult> Login(UserForLoginDto userForLoginDto){
           var userFromRepo=await repo.Login(userForLoginDto.Username.ToLower(),userForLoginDto.Password);
           if(userFromRepo==null)
           return Unauthorized();

           var claims=new[]{
                     new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                     new Claim(ClaimTypes.Name,userFromRepo.Username)
           };
           var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetSection("AppSettings:Token").Value));

           var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
          
          var tokenDescriptor=new SecurityTokenDescriptor{
              Subject=new ClaimsIdentity(claims),
              Expires= System.DateTime.Now.AddDays(1),
              SigningCredentials=creds
          };

          var tokenHandler=new JwtSecurityTokenHandler();
          var token=tokenHandler.CreateToken(tokenDescriptor);

          return Ok(new{
              token=tokenHandler.WriteToken(token)
          });

        }

    }
}