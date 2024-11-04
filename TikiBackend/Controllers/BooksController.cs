using BookCatalog.API.Model;
using BookCatalog.API.Models;
using BookCatalog.API.Queries.DTOs;
using BookCatalog.API.Queries.Mappers;
using BookCatalog.API.Repositories;
using Catalog.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BookCatalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookRepository bookRepository;
        private readonly ILogger<BooksController> logger;

        public BooksController(BookRepository bookRepository, ILogger<BooksController> logger)
        {
            this.bookRepository = bookRepository;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SearchBookAsync(
            [FromQuery] string searchWord,
            [FromQuery] BookFilter filter,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool isPriceAscend = true,
            [FromQuery] double startPrice = 0,
            [FromQuery] double endPrice = 0
            )
        {
            var predicate = BookFilter.BuildFilterPredicate(filter);
            Expression<Func<Book, bool>> pricePredicate = book => book.Price >= startPrice && book.Price <= endPrice;

            if (startPrice >= 0 && endPrice > 0 && startPrice < endPrice)
            {
                predicate = predicate.And(pricePredicate);
            }

            var itemsOnPageQuery = await bookRepository.SearchAsync(
                searchWord,
                predicate,
                !isPriceAscend,
                pageIndex,
                pageSize
                );

            if (itemsOnPageQuery.Data == null || itemsOnPageQuery.Data.Count == 0)
            {
                return NotFound("Books not found");
            }
            else
            {
                return Ok(new PaginatedItems<BookGeneralInfoDTO>(
                    itemsOnPageQuery.PageIndex,
                    itemsOnPageQuery.PageSize,
                    itemsOnPageQuery.TotalItems,
                    itemsOnPageQuery.Data.Select(
                        book => BookMapper.ToBookGeneralInfoDTO(book))
                    .ToList()));
            }
        }

        [AllowAnonymous]
        [HttpGet("lang-codes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllLanguageCodeAsync()
        {
            var langCodes = await bookRepository.GetConstants();

            if (langCodes == null || langCodes.Count == 0)
            {
                return NotFound("Language codes not found");
            }

            return Ok(langCodes);
        }

        [AllowAnonymous]
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBooksAsync(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10
            )
        {
            var totalItems = await bookRepository.LongCountAsync();

            var itemsOnPageQuery = await bookRepository.GetAllAsync(pageIndex, pageSize);

            if (itemsOnPageQuery.Data == null || itemsOnPageQuery.Data.Count == 0)
            {
                return NotFound("Books not found");
            }
            else
            {
                return Ok(new PaginatedItems<BookGeneralInfoDTO>(
                    itemsOnPageQuery.PageIndex,
                    itemsOnPageQuery.PageSize,
                    itemsOnPageQuery.TotalItems,
                    itemsOnPageQuery.Data.Select(
                        book => BookMapper.ToBookGeneralInfoDTO(book))
                    .ToList()));
            }
        }

        [AllowAnonymous]
        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBooksByFilterAsync(
            [FromQuery] BookFilter filter,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] double startPrice = 0,
            [FromQuery] double endPrice = 0,
            [FromQuery] bool isPriceAscend = true)
        {
            var predicate = BookFilter.BuildFilterPredicate(filter);
            Expression<Func<Book, object>> sortPredicate = book => book.Price;

            Expression<Func<Book, bool>> pricePredicate = book => book.Price >= startPrice && book.Price <= endPrice;

            if (startPrice >= 0 && endPrice > 0 && startPrice < endPrice)
            {
                predicate = predicate.And(pricePredicate);
            }

            var itemsOnPageQuery = await bookRepository.FindAsync(
                predicate,
                !isPriceAscend,
                pageIndex,
                pageSize);

            if (itemsOnPageQuery.Data == null || itemsOnPageQuery.Data.Count == 0)
            {
                return NotFound("Books not found");
            }
            else
            {
                return Ok(new PaginatedItems<BookGeneralInfoDTO>(
                    itemsOnPageQuery.PageIndex,
                    itemsOnPageQuery.PageSize,
                    itemsOnPageQuery.TotalItems,
                    itemsOnPageQuery.Data.Select(
                        book => BookMapper.ToBookGeneralInfoDTO(book)).ToList()));
            }
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute] int id = 1)
        {
            if (id <= 0)
            {
                return BadRequest("Id is not valid");
            }

            var book = await bookRepository.GetItemByIdAsync(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            return Ok(BookMapper.ToBookDetailDTO(book));
        }

        //[Authorize(Roles="Admin")]
        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO bookInfo)
        {
            Book book = BookMapper.ToBookFromCreateBookDTO(bookInfo);

            await bookRepository.AddAsync(book);
            await bookRepository.SaveChangesAsync();

            return Ok("Book created successfully");
        }

        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [Authorize]
        [HttpPatch("")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBookAsync([FromBody] BookDetailDTO book)
        {
            try
            {
                var currentBook = await bookRepository.GetItemByIdAsync(book.Id);

                if (currentBook == null)
                {
                    return BadRequest("Book for updated not found");
                }

                await bookRepository.Update(BookMapper.ToBookFromBookDetailDTO(book));
                await bookRepository.SaveChangesAsync();
                return Ok("Book updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [Authorize]
        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteBookAsync([FromQuery] int id)
        {
            try
            {
                await bookRepository.Remove(new Book { Id = id });
                await bookRepository.SaveChangesAsync();
                return Ok("Book deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
