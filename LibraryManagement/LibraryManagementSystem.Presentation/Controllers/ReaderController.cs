using LibraryManagementSystem.BusinessLogic.Services.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetAll()
        {
            var readers = await _readerService.GetAllAsync();
            return Ok(readers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reader>> GetById(int id)
        {
            var reader = await _readerService.GetByIdAsync(id);
            if (reader == null)
                return NotFound();

            return Ok(reader);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Reader reader)
        {
            await _readerService.AddAsync(reader);
            return Ok("Reader added.");
        }
    }
}
