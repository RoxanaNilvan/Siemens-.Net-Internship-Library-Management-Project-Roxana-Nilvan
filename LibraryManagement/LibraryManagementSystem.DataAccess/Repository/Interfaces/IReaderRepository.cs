using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.Repository.Interfaces
{
    public interface IReaderRepository
    {
        Task<IEnumerable<Reader>> GetAllAsync();
        Task<Reader?> GetByIdAsync(int id);
        Task AddAsync(Reader reader);
    }
}
