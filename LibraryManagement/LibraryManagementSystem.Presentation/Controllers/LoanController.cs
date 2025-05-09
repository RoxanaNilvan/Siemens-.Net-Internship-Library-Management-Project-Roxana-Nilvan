using LibraryManagementSystem.BusinessLogic.Services.Interfaces;
using LibraryManagementSystem.Domain.DTOS;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAll()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }



        [HttpPost]
        public async Task<ActionResult> CreateLoan([FromBody] LoanCreateDto dto)
        {
            try
            {
                var loan = new Loan
                {
                    ReaderId = dto.ReaderId,
                    BookId = dto.BookId,
                    BorrowDate = DateTime.UtcNow
                };

                await _loanService.CreateLoanAsync(loan);
                return Ok("Loan created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("reader/{readerId}")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetByReader(int readerId)
        {
            var loans = await _loanService.GetLoansByReaderAsync(readerId);
            return Ok(loans);
        }

        [HttpPost("return/{loanId}")]
        public async Task<ActionResult> ReturnLoan(int loanId)
        {
            try
            {
                await _loanService.ReturnLoanAsync(loanId);
                return Ok("Loan returned successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
