using LibraryManagementSystem.BusinessLogic.Services.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) 
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {
            await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest();

            await _bookService.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> Search(string? title, string? author)
        {
            var results = await _bookService.SearchBooksAsync(title ?? "", author ?? "");
            return Ok(results);
        }

        [HttpPost("borrow/{id}")]
        public async Task<ActionResult> Borrow(int id)
        {
            var success = await _bookService.BorrowBookAsync(id);
            if (!success)
                return BadRequest("Book not available for borrowing.");

            return Ok("Book borrowed successfully.");
        }

        [HttpPost("return/{id}")]
        public async Task<ActionResult> Return(int id)
        {
            var success = await _bookService.ReturnBookAsync(id);
            if (!success)
                return BadRequest("Cannot return this book.");

            return Ok("Book returned successfully.");
        }
    } 
}
