using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class BookIssueHistory
    {
        public int Id { get; set; }
        public int BookIssueId { get; set; }
        public BookIssue? BookIssue { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}