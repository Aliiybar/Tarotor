using AutoMapper;
using Tarotor.Entities;
using Tarotor.Models;

namespace Tarotor.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        
            CreateMap<SmtpVm, Smtp>().ReverseMap();
            CreateMap<TemplateVM, Template>().ReverseMap();
            CreateMap<ContentVm, Content>().ReverseMap();
            CreateMap<SocialMediaVm, SocialMedia>().ReverseMap();
            //// Domain to Resource
            //CreateMap<Music, MusicResource>();
            //CreateMap<Artist, ArtistResource>();

            //// Resource to Domain
            //CreateMap<MusicResource, Music>();
            //CreateMap<SaveMusicResource, Music>();
            //CreateMap<ArtistResource, Artist>();
            //CreateMap<SaveArtistResource, Artist>();
        }
    }
}