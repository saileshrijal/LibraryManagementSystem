
using LibraryManagementSystem.Dtos.BookDto;
using LibraryManagementSystem.Dtos.CategoryDto;
using LibraryManagementSystem.Dtos.StudentDto;

namespace LibraryManagementSystem.Services.Interface
{
    public interface IStudentService
    {
        Task CreateAsync(CreateStudentDto createStudentDto);
        Task UpdateAsync(UpdateStudentDto updateStudentDto);
        Task DeleteAsync(int id);
    }
}