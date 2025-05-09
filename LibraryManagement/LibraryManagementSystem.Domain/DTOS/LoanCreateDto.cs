using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.DTOS
{
    public class LoanCreateDto
    {
        public int ReaderId { get; set; }
        public int BookId { get; set; }
    }
}
