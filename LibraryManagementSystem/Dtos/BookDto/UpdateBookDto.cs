using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Dtos.BookDto
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publication { get; set; }
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int CategoryId { get; set; }
        public string? ModifiedUserId { get; set; }
    }
}