using Beezy.BackendTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beezy.BackendTest.Infrastructure.Data.DbContext
{
    public class BeezyCinemaContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BeezyCinemaContext()
        {
        }

        public BeezyCinemaContext(DbContextOptions<BeezyCinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cinema> Cinema { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Session> Session { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OpenSince).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Cinema)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cinema_City");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OriginalLanguage).HasMaxLength(255);

                entity.Property(e => e.OriginalTitle)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasMany(d => d.Session)
                    .WithOne(p => p.Movie);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.HasMany(m => m.Session)
                    .WithOne(s => s.Movie);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.GenreId });
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Cinema");

                entity.HasMany(r => r.Session)
                    .WithOne(s => s.Room);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Movie");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Room");
            });
        }
    }
}
