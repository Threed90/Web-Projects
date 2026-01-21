using Forum.Data.Models;
using Forum.Services.DTO;

namespace Forum.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAll();
        Task<PostViewModel> GetById(string id);
        Task Create(PostViewModel post);
        Task Update(string id, PostViewModel post);
        Task Delete(string id);
    }
}
