using Forum.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Seed
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
                new Post
                {
                    Id = "f3c0b4f1-8c7e-4c3d-9e3a-2b6f4e2a9d11",
                    Title = "Welcome to the Forum",
                    Content = "This is the first post in the forum. Feel free to share your thoughts!"
                },
                new Post
                {
                    Id = "9a7e2c55-4f0b-4b8e-8c2a-1d4f7b3e6a22",
                    Title = "Forum Guidelines",
                    Content = "Please be respectful to others and follow the community guidelines."
                }
            );
        }
    }
}
