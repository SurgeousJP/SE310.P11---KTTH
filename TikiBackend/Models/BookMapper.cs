using BookCatalog.API.Models;
using BookCatalog.API.Queries.DTOs;

namespace BookCatalog.API.Queries.Mappers
{
    public static class BookMapper

    {
        public static void MapBookToBook(Book descBook, Book book)
        {
            descBook.LanguageCode = book.LanguageCode;
            descBook.AverageRating = book.AverageRating;
            descBook.Description = book.Description;
            descBook.NumPages = book.NumPages;
            descBook.PublicationDay = book.PublicationDay;
            descBook.PublicationMonth = book.PublicationMonth;
            descBook.PublicationYear = book.PublicationYear;
            descBook.Isbn13 = book.Isbn13;
            descBook.ImageUrl = book.ImageUrl;
            descBook.RatingsCount = book.RatingsCount;
            descBook.Title = book.Title;
            descBook.TitleWithoutSeries = book.TitleWithoutSeries;
            descBook.Price = book.Price;
            descBook.Availability = book.Availability;
            descBook.Dimensions = book.Dimensions;
            descBook.DiscountPercentage = book.DiscountPercentage;
            descBook.ItemWeight = book.ItemWeight;
            descBook.FormatId = book.FormatId;
            descBook.AuthorName = book.AuthorName;
            descBook.PublisherId = book.PublisherId;
        }
        public static Book ToBookFromBookDetailDTO(BookDetailDTO book) => new Book
        {
            Id = (int)book.Id,
            LanguageCode = book.LanguageCode,
            AverageRating = (byte?)book.AverageRating,
            Description = book.Description,
            NumPages = (short?)book.NumPages,
            PublicationDay = (byte?)book.PublicationDay,
            PublicationMonth = (byte?)book.PublicationMonth,
            PublicationYear = book.PublicationYear,
            Isbn13 = 0,
            ImageUrl = book.ImageUrl,
            RatingsCount = (int?)book.RatingsCount,
            Title = book.Title,
            TitleWithoutSeries = book.TitleWithoutSeries,
            Price = book.Price,
            Availability = (short?)book.Availability,
            Dimensions = book.Dimensions,
            DiscountPercentage = book.DiscountPercentage,
            ItemWeight = book.ItemWeight,
            FormatId = (byte?)(long)book.FormatId.GetValueOrDefault(),
            AuthorName = book.AuthorName,
            PublisherId = (byte?)book.PublisherId,
        };

        public static Book ToBookFromCreateBookDTO(CreateBookDTO book)
        {
            Book b = new()
            {
                LanguageCode = book.LanguageCode,
                AverageRating = (byte?)book.AverageRating,
                Description = book.Description,
                NumPages = (short?)book.NumPages,
                PublicationDay = (byte?)book.PublicationDay,
                PublicationMonth = (byte?)book.PublicationMonth,
                PublicationYear = book.PublicationYear,
                Isbn13 = 0,
                ImageUrl = book.ImageUrl,
                RatingsCount = (int?)book.RatingsCount,
                Title = book.Title,
                TitleWithoutSeries = book.TitleWithoutSeries,
                Price = book.Price,
                Availability = (short?)book.Availability,
                Dimensions = book.Dimensions,
                DiscountPercentage = book.DiscountPercentage,
                ItemWeight = book.ItemWeight,
                FormatId = (byte?)book.FormatId,
                AuthorName = book.AuthorName,
                PublisherId = (byte?)book.PublisherId,
                GenreId = (byte)book.genre_Id,
            };
            return b;
        }
        public static BookGeneralInfoDTO ToBookGeneralInfoDTO(Book book)
        {
            return new BookGeneralInfoDTO()
            {
                Id = book.Id,
                Title = book.Title,
                TitleWithoutSeries = book.TitleWithoutSeries,
                AverageRating = book.AverageRating,
                Description = book.Description,
                NumPages = book.NumPages,
                PublicationDay = book.PublicationDay,
                PublicationMonth = book.PublicationMonth,
                PublicationYear = book.PublicationYear,
                ImageUrl = book.ImageUrl,
                Price = book.Price,
                Availability = book.Availability,
                DiscountPercentage = (float?)book.DiscountPercentage,
                AuthorName = book.AuthorName,
                RatingsCount = book.RatingsCount
            };
        }

        public static BookDetailDTO ToBookDetailDTO(Book book)
        {
            return new BookDetailDTO
            {
                Id = book.Id,
                Title = book.Title,
                TitleWithoutSeries = book.TitleWithoutSeries,
                AverageRating = book.AverageRating,
                Description = book.Description,
                NumPages = book.NumPages,
                PublicationDay = book.PublicationDay,
                PublicationMonth = book.PublicationMonth,
                PublicationYear = book.PublicationYear,
                ImageUrl = book.ImageUrl,
                Price = book.Price,
                Availability = book.Availability,
                DiscountPercentage = (float?)book.DiscountPercentage,
                AuthorName = book.AuthorName,
                RatingsCount = book.RatingsCount,
                LanguageCode = book.LanguageCode,
                Isbn13 = "",
                Dimensions = book.Dimensions,
                ItemWeight = book.ItemWeight,
            };
        }

        public static Genre ToGenre(CreateGenreDTO genre)
            => new Genre { Name = genre.Name };
    }
}
