using System;
using System.Collections.Generic;

namespace SkillShare.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }

        public string Skill { get; set; }

        public DateTime JoiningDate {get;set;}

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string Specialization { get; set; }

        public string City { get; set; }

        public string Country {get;set;}
 
        public ICollection<Photo> Photos {get;set;}

        public ICollection<Like> Likers{ get; set; }

        public ICollection<Like> Likees { get; set; }
    }
}