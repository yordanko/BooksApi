using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.OpenApi.Extensions;

namespace BooksApi.Controllers
{

    /// <summary>
    /// Books controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly BookService _booksService;
        public BooksController(BookService booksService)
        {
            _booksService = booksService;
        }

        /// <summary>
        /// Aggregation example. Returns a list of books by category. Swagger does not work with default parameters.  PLEASE USE POSTMAN FOR A TEST! 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <font color="red"> /api/books - return all available books,   /api/books/computers - return only computers book,   /api/books/cooking - only cooking books </font>
        /// </remarks>
        [HttpGet("{category:category?}")]
        public ActionResult<List<Book>> GetBookCategory(Category category) {
            var displayed = category.GetDisplayName();
            return _booksService.GetByCategory(displayed);
                }

        /// <summary>
        ///Return book with id
        /// </summary>
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _booksService.Get(id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _booksService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(Book bookIn)
        {
            var book = _booksService.Get(bookIn.Id);
            if(book == null)
            {
                return NotFound();
            }

            _booksService.Update(bookIn.Id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _booksService.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            _booksService.Remove(book);

            return NoContent();
        }

    }
}
