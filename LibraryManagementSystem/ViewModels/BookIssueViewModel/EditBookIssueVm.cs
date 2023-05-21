using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.ViewModels.BookIssueViewModel
{
    public class EditBookIssueVm
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public int StudentId { get; set; }
        public string? Note { get; set; }

        public List<SelectListItem>? BookSelectList { get; set; }
        public List<SelectListItem>? StudentSelectList { get; set; }
    }
}