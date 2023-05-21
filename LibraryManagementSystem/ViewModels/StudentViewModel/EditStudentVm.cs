using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.ViewModels.StudentViewModel
{
    public class EditStudentVm
    {
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        public IFormFile? Image { get; set; }
        public string? Grade { get; set; }

        public SelectList GradeSelectList() => new SelectList(Grades.Value, Grade);
    }
}