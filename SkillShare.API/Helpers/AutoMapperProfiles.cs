using System.Linq;
using AutoMapper;
using SkillShare.API.Dtos;
using SkillShare.API.Models;

namespace SkillShare.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForListDto>()
            .ForMember(dest=>dest.PhotoUrl,opt=>{
             opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).url);
            })
            .ForMember(dest=>dest.YearsOfExperience,opt=>{
              opt.ResolveUsing(d=>d.JoiningDate.CalculateAge());
            });
             CreateMap<User,UserForDetailsDto>()
              .ForMember(dest=>dest.PhotoUrl,opt=>{
             opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).url);
            })
            .ForMember(dest=>dest.YearsOfExperience,opt=>{
              opt.ResolveUsing(d=>d.JoiningDate.CalculateAge());
            });
             CreateMap<Photo,PhotosForDetailsDto>();
        }
    }
}