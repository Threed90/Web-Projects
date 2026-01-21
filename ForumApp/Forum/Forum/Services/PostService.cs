using Forum.Data;
using Forum.Services.DTO;
using Forum.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext _context;
        public PostService(ForumDbContext context)
        {
            this._context = context;
        }

        public async Task Create(PostViewModel post)
        {
            var entity = new Data.Models.Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = post.Title,
                Content = post.Content
            };

            await this._context.Posts.AddAsync(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var post = await this._context.Posts.FindAsync(id);

            if (post != null)
            {
                this._context.Posts.Remove(post);
                await this._context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PostViewModel>> GetAll()
        {
            var posts =
                await this._context.Posts
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .ToListAsync();

            return posts;
        }

        public async Task<PostViewModel> GetById(string id)
        {
            
            var post = 
                await this._context.Posts
                .Where(p => p.Id == id)
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task Update(string id, PostViewModel post)
        {
            var entity = await this._context.Posts.FindAsync(id);

            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Content = post.Content;
                this._context.Posts.Update(entity);
                await this._context.SaveChangesAsync();
            }
        }
    }
}
