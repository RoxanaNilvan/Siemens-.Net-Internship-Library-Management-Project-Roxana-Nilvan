using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repository.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.Repository
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly AppDbContext _context;

        public ReaderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Reader reader)
        {
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reader>> GetAllAsync()
        {
            return await _context.Readers.ToListAsync();
        }

        public async Task<Reader?> GetByIdAsync(int id)
        {
            return await _context.Readers.FindAsync(id);
        }
    }
}
