using IMDBAssignment.Models.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAssignment.Repositories.Context
{
    /// <summary>
    /// Repository context.
    /// </summary>
    public partial class RepositoryContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryContext()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">The database context options.</param>
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets and sets the Actors.
        /// </summary>
        public virtual DbSet<Actor> Actor { get; set; }

        /// <summary>
        /// Gets and sets the Movies.
        /// </summary>
        public virtual DbSet<Movie> Movie { get; set; }

        /// <summary>
        /// Gets and sets the MovieActors.
        /// </summary>
        public virtual DbSet<MovieActor> MovieActor { get; set; }

        /// <summary>
        /// Gets and sets the Producers.
        /// </summary>
        public virtual DbSet<Producer> Producer { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.Property(e => e.ActorName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Actor_Name");

                entity.Property(e => e.Bio).HasMaxLength(100);

                entity.Property(e => e.DateBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Birth");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.DateRelease)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Release");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.MovieName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Movie_Name");

                entity.Property(e => e.PosterPath)
                    .HasMaxLength(500)
                    .HasColumnName("Poster_Path");

                entity.Property(e => e.ProducerId).HasColumnName("Producer_Id");

                entity.HasOne(d => d.Producer)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.ProducerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_Producer");
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.ToTable("MovieActor");

                entity.Property(e => e.ActorId).HasColumnName("Actor_Id");

                entity.Property(e => e.MovieId).HasColumnName("Movie_Id");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.MovieActors)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieActor_Actor");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieActors)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieActor_Movie");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.ToTable("Producer");

                entity.Property(e => e.Bio).HasMaxLength(100);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Company_Name");

                entity.Property(e => e.DateBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Birth");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProducerName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Producer_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
