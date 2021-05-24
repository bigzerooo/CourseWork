using GameGenreNeuronNetwork.Models;
using Microsoft.EntityFrameworkCore;

namespace GameGenreNeuronNetwork.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<GameAspect> GameAspects { get; set; }
        public DbSet<GameGenre> GameGenres{ get; set; }
        public DbSet<GameAspectGroup> GameAspectGroups { get; set; }
        public DbSet<TrainingSet> TrainingSets { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<GameGenre>()
                .HasData(new[]
                 {
                    new GameGenre{ Id = 1, Name = "RPG" },
                    new GameGenre{ Id = 2, Name = "Strategy" },
                    new GameGenre{ Id = 3, Name = "Puzzle" },
                    new GameGenre{ Id = 4, Name = "Sport game" },
                    new GameGenre{ Id = 5, Name = "Shooter" },
                    new GameGenre{ Id = 6, Name = "Survival" },
                    new GameGenre{ Id = 7, Name = "Horror" },
                    new GameGenre{ Id = 8, Name = "Living simulator" },
                    new GameGenre{ Id = 9, Name = "Building Simulator" },
                 });


            modelBuilder.Entity<GameAspect>()
                .HasData(new[]
                 {
                    new GameAspect{ Id = 1, Name = "Blood" },
                    new GameAspect{ Id = 2, Name = "Shooting" },
                    new GameAspect{ Id = 3, Name = "Surviving" },
                    new GameAspect{ Id = 4, Name = "Scary" },
                    new GameAspect{ Id = 5, Name = "Building" },
                    new GameAspect{ Id = 6, Name = "Quests" },
                    new GameAspect{ Id = 7, Name = "Roleplaying" },
                    new GameAspect{ Id = 8, Name = "Logic thinking" },
                    new GameAspect{ Id = 9, Name = "Controlling one character" },
                    new GameAspect{ Id = 10, Name = "Sport elements" },
                 });


            modelBuilder.Entity<GameAspectGroup>()
                .HasKey(new[] { "GroupId", "GameAspectId" });

            modelBuilder.Entity<GameAspectGroup>()
                .HasOne(g => g.GameAspect)
                .WithMany(a => a.GameAspectGroups)
                .HasForeignKey(g => g.GameAspectId);

            modelBuilder.Entity<GameAspectGroup>()
                .HasData(new[]{
                    new GameAspectGroup { GroupId = 1, GameAspectId = 1, Value = 0 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 2, Value = 0 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 3, Value = 0 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 4, Value = 0 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 5, Value = 0 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 6, Value = 1 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 7, Value = 1 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 8, Value = 0 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 9, Value = 1 },
                    new GameAspectGroup { GroupId = 1, GameAspectId = 10, Value = 0 },
                });


            modelBuilder.Entity<TrainingSet>()
                .HasOne(s => s.GameGenre)
                .WithMany(g => g.TrainingSets)
                .HasForeignKey(s => s.GameGenreId);

            modelBuilder.Entity<TrainingSet>()
                .HasData(new[] {
                    new TrainingSet{ Id = 1, GameGenreId = 1, GameAspectGroupId = 1 }
                });
        }
    }
}
