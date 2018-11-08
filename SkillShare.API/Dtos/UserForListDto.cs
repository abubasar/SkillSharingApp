using System;

namespace SkillShare.API.Dtos
{
    public class UserForListDto
    {
         public int Id { get; set; } 
 
        public string Username { get; set; }

        public string Skill { get; set; }

        public int YearsOfExperience {get;set;}

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string City { get; set; }

        public string Country {get;set;}
 
       public string PhotoUrl {get;set;}
       
    }
}