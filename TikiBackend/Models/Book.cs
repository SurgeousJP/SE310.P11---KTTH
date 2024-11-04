using System;
using System.Collections.Generic;

namespace BookCatalog.API.Models;

public partial class Book
{
    public int Id { get; set; }

    public string? LanguageCode { get; set; }

    public byte? AverageRating { get; set; }

    public string? Description { get; set; }

    public short? NumPages { get; set; }

    public byte? PublicationDay { get; set; }

    public byte? PublicationMonth { get; set; }

    public short? PublicationYear { get; set; }

    public long? Isbn13 { get; set; }

    public string? Url { get; set; }

    public string? ImageUrl { get; set; }

    public int? RatingsCount { get; set; }

    public string? Title { get; set; }

    public string? TitleWithoutSeries { get; set; }

    public double? Price { get; set; }

    public short? Availability { get; set; }

    public string? Dimensions { get; set; }

    public double? DiscountPercentage { get; set; }

    public double? ItemWeight { get; set; }

    public byte? FormatId { get; set; }

    public byte? PublisherId { get; set; }

    public string? AuthorName { get; set; }

    public byte? BookId { get; set; }

    public byte GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;
}
