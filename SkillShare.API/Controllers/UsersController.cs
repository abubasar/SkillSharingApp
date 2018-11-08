using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillShare.API.Data;
using SkillShare.API.Dtos;

namespace SkillShare.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        private readonly ISkillRepository repo;
        private readonly IMapper mapper;

        public UsersController(ISkillRepository repo,
        IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(){
              var users=await repo.GetUsers();
              var usersToReturn=mapper.Map<IEnumerable<UserForListDto>>(users);
              return Ok(usersToReturn );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id){
            var user=await repo.GetUser(id);
            var userToReturn=mapper. Map<UserForDetailsDto>(user);
            return Ok(userToReturn);
        }

         
         
    }
}