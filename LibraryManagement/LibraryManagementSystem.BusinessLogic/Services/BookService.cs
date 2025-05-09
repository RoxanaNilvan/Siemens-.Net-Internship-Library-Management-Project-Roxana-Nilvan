using LibraryManagementSystem.BusinessLogic.Services.Interfaces;
using LibraryManagementSystem.DataAccess.Repository.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task AddBookAsync(Book book)
        {
            await _repository.AddAsync(book);
        }

        public async Task<bool> BorrowBookAsync(int bookId)
        {
            var book = await _repository.GetByIdAsync(bookId);

            if (book == null || book.Quantity <= 0)
                return false;

            book.Quantity -= 1;
            book.BorrowedCount += 1;
            await _repository.UpdateAsync(book);
            return true;
        }

        public async Task DeleteBookAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllAsync();
            return books;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            return book;
        }

        public async Task<bool> ReturnBookAsync(int bookId)
        {
            var book = await _repository.GetByIdAsync(bookId);

            if(book  == null || book.BorrowedCount <= 0) return false;

            book.Quantity += 1;
            book.BorrowedCount -= 1;
            await _repository.UpdateAsync(book);
            return true;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string title, string author)
        {
            var result = await _repository.SearchAsync(title, author);
            return result;
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _repository.UpdateAsync(book);
        }
    }
}
