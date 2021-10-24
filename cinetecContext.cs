using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CineTecBackend
{
    public partial class cinetecContext : DbContext
    {
        public cinetecContext()
        {
        }

        public cinetecContext(DbContextOptions<cinetecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieTheater> MovieTheaters { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=cinetec;Username=postgres;Password=bases2021");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Costa Rica.1252");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => new { e.OriginalMovieName, e.ActorName })
                    .HasName("actors_pkey");

                entity.ToTable("actors");

                entity.Property(e => e.OriginalMovieName)
                    .HasMaxLength(20)
                    .HasColumnName("original_movie_name");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(20)
                    .HasColumnName("actor_name");

                entity.HasOne(d => d.OriginalMovieNameNavigation)
                    .WithMany(p => p.Actors)
                    .HasForeignKey(d => d.OriginalMovieName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("actors_movie_fk");
            });

            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("cinema_pkey");

                entity.ToTable("cinema");

                entity.Property(e => e.Number)
                    .ValueGeneratedNever()
                    .HasColumnName("number");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Columns).HasColumnName("columns");

                entity.Property(e => e.NameMovieTheater)
                    .HasMaxLength(20)
                    .HasColumnName("name_movie_theater");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.HasOne(d => d.NameMovieTheaterNavigation)
                    .WithMany(p => p.Cinemas)
                    .HasForeignKey(d => d.NameMovieTheater)
                    .HasConstraintName("cinema_movie_theater_fk");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.SecLastName)
                    .HasMaxLength(20)
                    .HasColumnName("sec_last_name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.OriginalName)
                    .HasName("movie_pkey");

                entity.ToTable("movie");

                entity.Property(e => e.OriginalName)
                    .HasMaxLength(20)
                    .HasColumnName("original_name");

                entity.Property(e => e.Director)
                    .HasMaxLength(20)
                    .HasColumnName("director");

                entity.Property(e => e.Gendre)
                    .HasMaxLength(20)
                    .HasColumnName("gendre");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(350)
                    .HasColumnName("image_url");

                entity.Property(e => e.Lenght).HasColumnName("lenght");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<MovieTheater>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("movie_theater_pkey");

                entity.ToTable("movie_theater");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.CinemaAmount).HasColumnName("cinema_amount");

                entity.Property(e => e.Location)
                    .HasMaxLength(20)
                    .HasColumnName("location");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("purchase");

                entity.Property(e => e.Purchaseid)
                    .ValueGeneratedNever()
                    .HasColumnName("purchaseid");

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Movieoriginalname)
                    .HasMaxLength(20)
                    .HasColumnName("movieoriginalname");

                entity.Property(e => e.Screeningid).HasColumnName("screeningid");

                entity.Property(e => e.Theatername)
                    .HasMaxLength(20)
                    .HasColumnName("theatername");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Clientid)
                    .HasConstraintName("purchase_client_fk");

                entity.HasOne(d => d.MovieoriginalnameNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Movieoriginalname)
                    .HasConstraintName("purchase_movie_fk");

                entity.HasOne(d => d.Screening)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Screeningid)
                    .HasConstraintName("purchase_screening_fk");

                entity.HasOne(d => d.TheaternameNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.Theatername)
                    .HasConstraintName("purchase_movie_theater_fk");
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.ToTable("screening");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.CinemaNumber).HasColumnName("cinema_number");

                entity.Property(e => e.Hour).HasColumnName("hour");

                entity.Property(e => e.MovieOriginalName)
                    .HasMaxLength(20)
                    .HasColumnName("movie_original_name");

                entity.HasOne(d => d.CinemaNumberNavigation)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.CinemaNumber)
                    .HasConstraintName("screening_cinema_fk");

                entity.HasOne(d => d.MovieOriginalNameNavigation)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.MovieOriginalName)
                    .HasConstraintName("screening_movie_fk");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(e => new { e.ScreeningId, e.RowNum, e.ColumnNum })
                    .HasName("seat_pkey");

                entity.ToTable("seat");

                entity.Property(e => e.ScreeningId).HasColumnName("screening_id");

                entity.Property(e => e.RowNum).HasColumnName("row_num");

                entity.Property(e => e.ColumnNum).HasColumnName("column_num");

                entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .HasColumnName("state");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.PurchaseId)
                    .HasConstraintName("seat_purchase_fk");

                entity.HasOne(d => d.Screening)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.ScreeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("seat_screening_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
