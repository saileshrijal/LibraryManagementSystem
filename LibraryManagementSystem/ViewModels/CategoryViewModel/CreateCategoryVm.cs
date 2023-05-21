using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels.CategoryViewModel
{
    public class CreateCategoryVm
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}