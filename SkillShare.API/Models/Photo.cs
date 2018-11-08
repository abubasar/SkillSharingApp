using System;

namespace SkillShare.API.Models
{
    public class Photo
    {
        public int Id {get;set;}

        public string url { get; set; }

        public DateTime DateAdded {get;set;}

        public bool IsMain {get;set;}

        public User User { get; set; }

        public int UserId { get; set; }


    }
}