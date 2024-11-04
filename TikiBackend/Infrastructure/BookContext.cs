using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Infrastructure;

public partial class BookContext : DbContext
{
    public BookContext()
    {
    }

    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BookCatalog;Integrated Security=True;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");

            entity.Property(e => e.Id)
                 .ValueGeneratedOnAdd()  // Change this line
                 .HasColumnName("id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasColumnName("author_name");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.AverageRating).HasColumnName("average_rating");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Description)
                .HasMaxLength(2850)
                .HasColumnName("description");
            entity.Property(e => e.Dimensions)
                .HasMaxLength(50)
                .HasColumnName("dimensions");
            entity.Property(e => e.DiscountPercentage).HasColumnName("discount_percentage");
            entity.Property(e => e.FormatId).HasColumnName("format_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(100)
                .HasColumnName("image_url");
            entity.Property(e => e.Isbn13).HasColumnName("isbn13");
            entity.Property(e => e.ItemWeight).HasColumnName("item_weight");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(50)
                .HasColumnName("language_code");
            entity.Property(e => e.NumPages).HasColumnName("num_pages");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PublicationDay).HasColumnName("publication_day");
            entity.Property(e => e.PublicationMonth).HasColumnName("publication_month");
            entity.Property(e => e.PublicationYear).HasColumnName("publication_year");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.RatingsCount).HasColumnName("ratings_count");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.TitleWithoutSeries)
                .HasMaxLength(200)
                .HasColumnName("title_without_series");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .HasColumnName("url");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Genres");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genres");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
