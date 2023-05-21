using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModels.BookIssueViewModel
{
    public class BookIssueHistoryVm
    {
        public int Id { get; set; }
        public int BookIssueId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}