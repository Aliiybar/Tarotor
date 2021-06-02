using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Tarotor.DAL.Repositories.SocialMedia;
using Tarotor.Entities;
using Tarotor.Models;

namespace Tarotor.Services
{
    public interface ISocialMediaManager
    {
        SocialMediaVm GetSocialMedia();
        Task SaveSocialMedia(SocialMediaVm socialMediaVm);
    }
    public class SocialMediaManager :ISocialMediaManager
    {
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public SocialMediaManager(ISocialMediaRepository socialMediaRepository,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            _socialMediaRepository = socialMediaRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public SocialMediaVm GetSocialMedia()
        {
            var retVal = new SocialMediaVm();
            var cachedResult = _memoryCache.Get<SocialMediaVm>("socialMedia");
            if (cachedResult == null)
            {
                var medias = _socialMediaRepository.GetAll();
                if (medias.Any())
                {
                    retVal =   _mapper.Map<SocialMediaVm>(medias.First());
                }

                _memoryCache.Set("socialMedia", retVal);
            }
            else
            {
                retVal = cachedResult;
            }
 
          

            return retVal;
        }

        public async Task SaveSocialMedia(SocialMediaVm socialMediaVm)
        {
            var socialMedia = new SocialMedia();
            var medias = _socialMediaRepository.GetAll();
            if (medias.Any())
            {
                socialMedia = medias.First();
            }
            else
            {
                socialMedia.Id = Guid.NewGuid().ToString();
            }

            socialMedia.Facebook = socialMediaVm.Facebook;
            socialMedia.Twitter = socialMediaVm.Twitter;
            socialMedia.Instagram = socialMediaVm.Instagram;
            socialMedia.Youtube = socialMediaVm.Youtube;
            await _socialMediaRepository.UpdateAsync(socialMedia);
             
            _memoryCache.Remove("socialMedia");
        }
    }
}