using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publication { get; set; }
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? CreatedUserId { get; set; }
        public ApplicationUser? CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedUserId { get; set; }
        public ApplicationUser? ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}