using Microsoft.EntityFrameworkCore;
using SkillShare.API.Models;

namespace SkillShare.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) {}
              
              public DbSet<Value> Values { get; set; }


    }
}