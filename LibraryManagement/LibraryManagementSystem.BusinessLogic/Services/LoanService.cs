using LibraryManagementSystem.BusinessLogic.Services.Interfaces;
using LibraryManagementSystem.DataAccess.Repository.Interfaces;
using LibraryManagementSystem.Domain.DTOS;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BusinessLogic.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;

        public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
        }
        public async Task CreateLoanAsync(Loan loan)
        {
            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book == null || book.Quantity <= 0)
            {
                throw new InvalidOperationException("Book not available for borrowing.");
            }

            book.Quantity -= 1;
            await _bookRepository.UpdateAsync(book);

            loan.BorrowDate = DateTime.UtcNow;
            loan.ReturnDate = null;
            await _loanRepository.AddAsync(loan);
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _loanRepository.GetAllAsync();

            var result = loans.Select(loan => new LoanDto
            {
                Id = loan.Id,
                BookTitle = loan.Book.Title,
                ReaderName = loan.Reader.Name,
                BorrowDate = loan.BorrowDate,
                ReturnDate = loan.ReturnDate
            });

            return result;
        }


        public async Task<IEnumerable<LoanDto>> GetLoansByReaderAsync(int readerId)
        {
            var loans = await _loanRepository.GetLoansByReaderIdAsync(readerId);

            var result = loans.Select(loan => new LoanDto
            {
                Id = loan.Id,
                BookTitle = loan.Book.Title,
                ReaderName = loan.Reader.Name,
                BorrowDate = loan.BorrowDate,
                ReturnDate = loan.ReturnDate
            });

            return result;
        }

        public async Task ReturnLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null || loan.ReturnDate != null)
                throw new InvalidOperationException("Loan not found or already returned.");

            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book != null)
            {
                book.Quantity += 1;
                await _bookRepository.UpdateAsync(book);
            }

            loan.ReturnDate = DateTime.UtcNow;
            await _loanRepository.UpdateAsync(loan);
        }
    }
}
