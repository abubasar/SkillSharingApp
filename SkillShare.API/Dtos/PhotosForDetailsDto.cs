using System;

namespace SkillShare.API.Dtos
{
    public class PhotosForDetailsDto
    {
          public int Id {get;set;}

        public string url { get; set; }

        public DateTime DateAdded {get;set;}

        public bool IsMain {get;set;}
    }
}