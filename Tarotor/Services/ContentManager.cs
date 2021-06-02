using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tarotor.DAL.Repositories.Content;
using Tarotor.Entities;
using Tarotor.Models;

namespace Tarotor.Services
{
    public class ContentManager : IContentManager
    {
        private readonly IMapper _mapper;
        private readonly IContentRepository _contentRepository;

        public ContentManager(IMapper mapper,
            IContentRepository contentRepository)
        {
            _mapper = mapper;
            _contentRepository = contentRepository;
        }

        public List<ContentVm> GetContents()
        {
            var contents =  _contentRepository.GetAll();
            return _mapper.Map<List<ContentVm>>(contents);
        }

        public async Task<ContentVm> GetContentAsync(string id)
        {
            var content = await _contentRepository.GetAsync(id);
            return _mapper.Map<ContentVm>(content);
        }

        public ContentVm GetContentByName(string contentName, string language)
        {
            var contentCollection =  _contentRepository.FindBy(k=>k.ContentName == contentName && k.Language == language);
            Content content = null;
            if (contentCollection.Any())
            {
                content = contentCollection.First();
            }
            return content != null
                ? _mapper.Map<ContentVm>(content)
                : null;
        }
        
        public async Task<string> SaveContentAsync(ContentVm contentVm)
        {
            var content = new Content();
            var id = contentVm.Id;
            if (string.IsNullOrWhiteSpace(contentVm.Id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
               content = await _contentRepository.GetAsync(id);
            }

            content.Id = id;
            content.ContentName = contentVm.ContentName;
            content.ContentTitle = contentVm.ContentTitle;
            content.ContentBody = contentVm.ContentBody.Replace("'", "&apos;");
            content.Language = contentVm.Language;
            if (string.IsNullOrWhiteSpace(contentVm.Id))
            {
                _contentRepository.Add(content);
                await _contentRepository.SaveAsync();
            }
            else
            {
                await _contentRepository.UpdateAsync(content);
            }

            return content.Id;
        }

    }
}