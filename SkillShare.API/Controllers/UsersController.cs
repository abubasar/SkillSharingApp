using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillShare.API.Data;
using SkillShare.API.Dtos;
using SkillShare.API.Helpers;
using SkillShare.API.Models;

namespace SkillShare.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
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
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams){
              var currentUserId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
              var userFromRepo=await repo.GetUser(currentUserId);
              userParams.UserId=currentUserId;
              var users=await repo.GetUsers(userParams);
              var usersToReturn=mapper.Map<IEnumerable<UserForListDto>>(users);
              Response.AddPagination(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages);
                 return Ok(usersToReturn);
        }

        [HttpGet("{id}",Name="GetUser")]
        public async Task<IActionResult> GetUser(int id){
            var user=await repo.GetUser(id);
            var userToReturn=mapper.Map<UserForDetailsDto>(user);
            return Ok(userToReturn);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateUser(int id,UserForUpdateDto userForUpdateDto){
          if(id!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
          return Unauthorized();

          var userFromRepo=await repo.GetUser(id);
          mapper.Map(userForUpdateDto,userFromRepo);

          if(await repo.SaveAll()) 
          return NoContent();

          throw new Exception($"Updating user {id} failed on save");


        }

        [HttpPost("{id}/like/{recipientId}")]

        public async Task<IActionResult> LikeUser(int id,int recipientId){
             if(id!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
          return Unauthorized();

          var like=await repo.GetLike(id,recipientId);

          if(like!=null)
          return BadRequest("You already Follow this Guy");

          if(await repo.GetUser(recipientId)==null)
          return NotFound(); 

          like=new Like
          {
             LikerId=id,
             LikeeId=recipientId                   
          };

          repo.Add<Like>(like);

          if(await repo.SaveAll())
              return Ok();
          return BadRequest("Failed to like User");


           

        }



         
         
    }
}