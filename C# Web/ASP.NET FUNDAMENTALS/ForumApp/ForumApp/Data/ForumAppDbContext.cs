using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data
{
    using Data.Models;

    public class ForumAppDbContext : DbContext
    {
        private Post FirstPost { get; set; } = null!;
        private Post SecondPost { get; set; } = null!;
        private Post ThirdPost { get; set; } = null!;

        public ForumAppDbContext()
        { }

        public ForumAppDbContext(DbContextOptions options)
            : base(options)
        {
            Database.Migrate();
        }

        public virtual DbSet<Post> Posts { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedPosts();
            modelBuilder
                .Entity<Post>()
                .HasData(FirstPost,
                         SecondPost,
                         ThirdPost);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedPosts()
        {
            FirstPost = new Post
            {
                Id = 1,
                Title = "My first post",
                Content = "My first post will be about performing CRUD operations in MVC. Its so much fun!"
            };

            SecondPost = new Post
            {
                Id = 2,
                Title = "My second post",
                Content = "This is my second post. CRUD operations in MVC are getting more and more interesting!"
            };

            ThirdPost = new Post
            {
                Id = 3,
                Title = "My third post",
                Content = "Hello there! I'm getting better and better with the CRUD operations in MVC. Stay tuned!"
            };
        }
    }
}