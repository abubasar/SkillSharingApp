using System;
using System.Collections.Generic;
using SkillShare.API.Models;

namespace SkillShare.API.Dtos
{
    public class UserForDetailsDto
    {
         public int Id { get; set; }

        public string Username { get; set; }

        public string Skill { get; set; }

        public int YearsOfExperience {get;set;}

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string Specialization { get; set; }

        public string City { get; set; }

        public string Country {get;set;}
 
       public string PhotoUrl { get; set; }

       public ICollection<PhotosForDetailsDto> Photos {get;set;}
    }
}