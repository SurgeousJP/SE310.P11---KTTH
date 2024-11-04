using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Queries.DTOs
{
    public class CreateBookDTO
    {
        public int Id { get; set; }

        public string LanguageCode { get; set; } = null!;

        public float? AverageRating { get; set; }

        public string? Description { get; set; }

        public long? NumPages { get; set; }

        public short? PublicationDay { get; set; }

        public short? PublicationMonth { get; set; }

        public short? PublicationYear { get; set; }

        public string? Isbn13 { get; set; }

        public string? ImageUrl { get; set; }

        public long? RatingsCount { get; set; }

        public string? Title { get; set; }

        public string? TitleWithoutSeries { get; set; }

        public double? Price { get; set; }

        public double? Availability { get; set; }

        public string? Dimensions { get; set; }

        public float? DiscountPercentage { get; set; }

        public double? ItemWeight { get; set; }

        public string? AuthorName { get; set; }

        [Required]
        public long FormatId { get; set; }

        [Required]
        public long PublisherId { get; set; }

        public int genre_Id { get; set; }
    }
}
