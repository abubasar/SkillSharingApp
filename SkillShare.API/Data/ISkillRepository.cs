using System.Collections.Generic;
using System.Threading.Tasks;
using SkillShare.API.Helpers;
using SkillShare.API.Models;

namespace SkillShare.API.Data
{
    public interface ISkillRepository
    {
         void Add<T> (T entity) where T:class;

         void Delete<T>(T entity) where T:class;

         Task<bool> SaveAll();

         Task<PagedList<User>> GetUsers(UserParams userParams);

         Task<User> GetUser(int id);

         Task<Like> GetLike(int userId,int recipientId);
    }
}