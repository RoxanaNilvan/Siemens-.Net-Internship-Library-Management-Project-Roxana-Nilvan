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
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _repository;

        public ReaderService(IReaderRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Reader reader)
        {
            await _repository.AddAsync(reader);
        }

        public async Task<IEnumerable<Reader>> GetAllAsync()
        {
            var readers = await _repository.GetAllAsync();
            return readers;
        }

        public async Task<Reader?> GetByIdAsync(int id)
        {
            var reader = await _repository.GetByIdAsync(id);
            return reader;
        }
    }
}
