using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OneToOneOneToManyManToMany.Models;

namespace OneToOneOneToManyManToMany.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext( DbContextOptions<DataContext> options): base(options){}




        public DbSet<Character> Characters { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Faction> Factions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between Character and Backpack
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Backpack)
                .WithOne(b => b.Character)
                .HasForeignKey<Backpack>(b => b.CharacterId);

            // Define the relationship between Character and Weapons
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Weapons)
                .WithOne(w => w.Character)
                .HasForeignKey(w => w.CharacterId);

            // Define the relationship between Character and Factions
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Factions)
                .WithMany(f => f.Characters)
                .UsingEntity(j => j.ToTable("CharacterFactions"));

            // Define the relationship between Weapon and Character
            modelBuilder.Entity<Weapon>()
                .HasOne(w => w.Character)
                .WithMany(c => c.Weapons)
                .HasForeignKey(w => w.CharacterId);

            // Define the relationship between Faction and Character
            modelBuilder.Entity<Faction>()
                .HasMany(f => f.Characters)
                .WithMany(c => c.Factions)
                .UsingEntity(j => j.ToTable("CharacterFactions"));
        }

    }
}
