using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModels.BookIssueViewModel
{
    public class BookIssueVm
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string? StudentName { get; set; }
        public string? Book { get; set; }
        public string? Publication { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}