using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModels.BookIssueViewModel
{
    public class CreateBookIssueHistoryVm
    {
        [Required]
        public int BookIssueId { get; set; }
        [Required]
        public string? Message { get; set; }
    }
}