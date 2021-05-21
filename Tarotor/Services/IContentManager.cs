using System.Collections.Generic;
using System.Threading.Tasks;
using Tarotor.Models;

namespace Tarotor.Services
{
    public interface IContentManager
    {
        List<ContentVm> GetContents();
        Task<ContentVm> GetContentAsync(string id);
        ContentVm GetContentByName(string contentName, string language);
        Task<string> SaveContentAsync(ContentVm contentVm);
    }
}