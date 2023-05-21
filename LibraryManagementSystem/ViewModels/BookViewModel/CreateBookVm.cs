using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.ViewModels.BookViewModel
{
    public class CreateBookVm
    {
        [Required]
        public string? Name { get; set; }
        public string? Author { get; set; }
        [Required]
        public string? Publication { get; set; }
        [Required]
        public int NumberOfCopies { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? CreatedUserId { get; set; }

        [ValidateNever]
        public List<SelectListItem>? Categories { get; set; }
    }
}