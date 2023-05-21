using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.ViewModels.BookViewModel
{
    public class EditBookVm
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Author { get; set; }
        [Required]
        public string? Publication { get; set; }
        [Required]
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ValidateNever]
        public List<SelectListItem>? Categories { get; set; }
    }
}