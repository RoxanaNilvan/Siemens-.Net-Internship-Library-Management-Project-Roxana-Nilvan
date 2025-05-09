using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BusinessLogic.Services.Interfaces
{
    public interface IReaderService
    {
        Task<IEnumerable<Reader>> GetAllAsync();
        Task<Reader?> GetByIdAsync(int id);
        Task AddAsync(Reader reader);
    }
}
