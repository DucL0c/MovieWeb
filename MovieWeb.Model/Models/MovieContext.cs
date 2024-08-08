using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MovieWeb.Model.Models;

public partial class MovieContext : DbContext
{
    public MovieContext()
    {
    }

    public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Filmmaker> Filmmakers { get; set; }

    public virtual DbSet<FilmmakersDepartment> FilmmakersDepartments { get; set; }

    public virtual DbSet<FilmmakersSocial> FilmmakersSocials { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenreMovie> GenreMovies { get; set; }

    public virtual DbSet<GroupRole> GroupRoles { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieFilmmaker> MovieFilmmakers { get; set; }

    public virtual DbSet<PointStar> PointStars { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Social> Socials { get; set; }

    public virtual DbSet<Source> Sources { get; set; }

    public virtual DbSet<SystemGroup> SystemGroups { get; set; }

    public virtual DbSet<SystemRole> SystemRoles { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserEpisode> UserEpisodes { get; set; }

    public virtual DbSet<WatchList> WatchLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LAPTOP-ND4MU73N\\SQLEXPRESS;Database=Movie;TrustServerCertificate=True;User ID=sa;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC072752DC99");

            entity.ToTable("Department");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Episodes__3214EC0770DAEA65");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NameEpisodes)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Movie).WithMany(p => p.Episodes)
                .HasForeignKey(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKEpisodes387818");
        });

        modelBuilder.Entity<Filmmaker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Filmmake__3214EC07A52D9F79");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FilmmakersDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Filmmake__3214EC0748CC5395");

            entity.ToTable("Filmmakers_Department");

            entity.HasOne(d => d.Department).WithMany(p => p.FilmmakersDepartments)
                .HasForeignKey(d => d.Departmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFilmmakers161037");

            entity.HasOne(d => d.Filmmakers).WithMany(p => p.FilmmakersDepartments)
                .HasForeignKey(d => d.Filmmakersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFilmmakers138758");
        });

        modelBuilder.Entity<FilmmakersSocial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Filmmake__3214EC07D7C447AB");

            entity.ToTable("Filmmakers_Social");

            entity.HasOne(d => d.Filmmakers).WithMany(p => p.FilmmakersSocials)
                .HasForeignKey(d => d.Filmmakersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFilmmakers123563");

            entity.HasOne(d => d.Social).WithMany(p => p.FilmmakersSocials)
                .HasForeignKey(d => d.Socialid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFilmmakers155151");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC0749D37E75");

            entity.ToTable("Genre");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GenreMovie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre_Mo__3214EC0789299836");

            entity.ToTable("Genre_Movie");

            entity.HasOne(d => d.Genre).WithMany(p => p.GenreMovies)
                .HasForeignKey(d => d.Genreid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKGenre_Movi671177");

            entity.HasOne(d => d.Movie).WithMany(p => p.GenreMovies)
                .HasForeignKey(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKGenre_Movi357054");
        });

        modelBuilder.Entity<GroupRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Group_Ro__3214EC0757E85B50");

            entity.ToTable("Group_Role");

            entity.HasOne(d => d.SystemGroup).WithMany(p => p.GroupRoles)
                .HasForeignKey(d => d.SystemGroupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKGroup_Role143799");

            entity.HasOne(d => d.SystemRole).WithMany(p => p.GroupRoles)
                .HasForeignKey(d => d.SystemRoleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKGroup_Role178105");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movie__3214EC072890E648");

            entity.ToTable("Movie");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EnglishName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsLock)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Overview)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Trailer)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MovieFilmmaker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movie_Fi__3214EC07DBB60AF7");

            entity.ToTable("Movie_Filmmakers");

            entity.Property(e => e.FilmmakersDepartmentid).HasColumnName("Filmmakers_Departmentid");
            entity.Property(e => e.NameInMovie)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.FilmmakersDepartment).WithMany(p => p.MovieFilmmakers)
                .HasForeignKey(d => d.FilmmakersDepartmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMovie_Film127369");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieFilmmakers)
                .HasForeignKey(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMovie_Film685669");
        });

        modelBuilder.Entity<PointStar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PointSta__3214EC070D32B020");

            entity.ToTable("PointStar");

            entity.HasOne(d => d.Movie).WithMany(p => p.PointStars)
                .HasForeignKey(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPointStar575006");

            entity.HasOne(d => d.User).WithMany(p => p.PointStars)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPointStar965608");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3214EC07D98DDBF1");

            entity.ToTable("Review");

            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKReview402778");

            entity.HasOne(d => d.ReviewNavigation).WithMany(p => p.InverseReviewNavigation)
                .HasForeignKey(d => d.Reviewid)
                .HasConstraintName("FKReview460555");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKReview971802");
        });

        modelBuilder.Entity<Social>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Social__3214EC07F4AF0DB7");

            entity.ToTable("Social");

            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Source__3214EC0743BE4F0E");

            entity.ToTable("Source");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Quality)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SourceFilm)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Episode).WithMany(p => p.Sources)
                .HasForeignKey(d => d.Episodeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSource534632");
        });

        modelBuilder.Entity<SystemGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemGr__3214EC07BDB26570");

            entity.ToTable("SystemGroup");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GroupCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SystemRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemRo__3214EC07F4F745AD");

            entity.ToTable("SystemRole");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NameRole)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemUs__3214EC07FD0F485F");

            entity.ToTable("SystemUser");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.SystemGroup).WithMany(p => p.SystemUsers)
                .HasForeignKey(d => d.SystemGroupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSystemUser257749");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC072FE5A0E8");

            entity.ToTable("User");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsPay)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifierBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserEpisode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_Epi__3214EC07EF0F93C4");

            entity.ToTable("User_Episodes");

            entity.HasOne(d => d.Episodes).WithMany(p => p.UserEpisodes)
                .HasForeignKey(d => d.Episodesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser_Episo546482");

            entity.HasOne(d => d.User).WithMany(p => p.UserEpisodes)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser_Episo305028");
        });

        modelBuilder.Entity<WatchList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WatchLis__3214EC07E6BA7D4F");

            entity.ToTable("WatchList");

            entity.HasOne(d => d.Movie).WithMany(p => p.WatchLists)
                .HasForeignKey(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWatchList991613");

            entity.HasOne(d => d.User).WithMany(p => p.WatchLists)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWatchList439361");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
