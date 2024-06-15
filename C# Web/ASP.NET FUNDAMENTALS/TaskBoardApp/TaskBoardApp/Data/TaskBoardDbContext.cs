using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Models;
using Tasks = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data
{
    public class TaskBoardDbContext : IdentityDbContext<IdentityUser>
    {
        private IdentityUser TestUser { get; set; }

        private Board OpenBoard { get; set; }

        private Board  InProgressBoard { get; set; }

        private Board DoneBoard { get; set; }

        public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Tasks>()
                .HasOne(t => t.Board)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder
                .Entity<IdentityUser>()
                    .HasData(TestUser);


            SeedBoards();
            builder
                .Entity<Board>()
                    .HasData(OpenBoard, InProgressBoard, DoneBoard);

            builder
                .Entity<Tasks>()
                    .HasData(new Tasks
                    {
                        Id = 1,
                        Title = "Imrpove CSS styles",
                        Description = "string string string string strring string string string",
                        CreatedOn = DateTime.Now.AddMonths(-3),
                        OwnerId = TestUser.Id,
                        BoardId = OpenBoard.Id
                    },
                    new Tasks
                    {
                        Id = 2,
                        Title = "Android Client App",
                        Description = "text string string string text string text text text string text",
                        CreatedOn = DateTime.Now.AddYears(-1),
                        OwnerId = TestUser.Id,
                        BoardId = OpenBoard.Id
                    },
                    new Tasks
                    {
                        Id = 3,
                        Title =  "Desktop Client App",
                        Description = "Text string text string string string string text",
                        CreatedOn = DateTime.Now.AddMonths(-1),
                        OwnerId = TestUser.Id,
                        BoardId = InProgressBoard.Id
                    },
                    new Tasks
                    {
                        Id = 4,
                        Title = "Create Tasks",
                        Description = "String Text text String , more string, text",
                        CreatedOn = DateTime.Now.AddDays(-234),
                        OwnerId = TestUser.Id,
                        BoardId = DoneBoard.Id
                    });

            base.OnModelCreating(builder);
        }

        private void SeedBoards()
        {
            OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };
            InProgressBoard = new Board()
            {
                Id = 2,
                Name = "In Progress"
            };
            DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            TestUser = new IdentityUser()
            {
                UserName = "test@softuni.bg",
                NormalizedUserName = "TEST@SOFTUNI.BG"
            };
            TestUser.PasswordHash = hasher.HashPassword(TestUser, "softuni");
        }
    }
}
