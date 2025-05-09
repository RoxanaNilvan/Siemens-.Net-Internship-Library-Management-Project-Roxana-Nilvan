using LibraryManagementSystem.Domain.DTOS;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BusinessLogic.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<IEnumerable<LoanDto>> GetLoansByReaderAsync(int readerId);
        Task CreateLoanAsync(Loan loan);
        Task ReturnLoanAsync(int loanId);
    }
}
