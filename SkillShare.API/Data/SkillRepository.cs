using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillShare.API.Helpers;
using SkillShare.API.Models;

namespace SkillShare.API.Data
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DataContext context;

        public SkillRepository(DataContext context)
        {
            this.context = context;
        }

        

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           context.Remove(entity);
        }
//recipient=likee
        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await context.Likes.FirstOrDefaultAsync(u=>
            u.LikerId==userId && u.LikeeId==recipientId);
        }

        public async Task<User> GetUser(int id)
        {
            var user= await context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Id==id);
         return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users= context.Users.Include(p=>p.Photos).AsQueryable();

            users=users.Where(u=>u.Id !=userParams.UserId);
            

            if(userParams.Likers){
               var userLikers=await GetUserLikes(userParams.UserId,userParams.Likers);                               
               users= users.Where(u=>userLikers.Contains(u.Id));
            }

             if(userParams.Likees){
                var userLikees=await GetUserLikes(userParams.UserId,userParams.Likers);                               
               users=users.Where(u=>userLikees.Contains(u.Id));
            }
              return await PagedList<User>.Createasync(users,userParams.PageNumber,userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id,bool likers){
            var user=await context.Users
            .Include(x=>x.Likers)
            .Include(x=>x.Likees).FirstOrDefaultAsync(u=>u.Id==id);

            if(likers){
                return  user.Likers.Where(u=>u.LikeeId==id).Select(i=>i.LikerId);
            }else{
                 return user.Likees.Where(u=>u.LikerId==id).Select(i=>i.LikeeId);
            }
        }

        public async  Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync()>0;
        }

       
    }
}