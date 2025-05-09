using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
