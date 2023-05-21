
using LibraryManagementSystem.Dtos.BookDto;
using LibraryManagementSystem.Dtos.CategoryDto;

namespace LibraryManagementSystem.Services.Interface
{
    public interface IBookService
    {
        Task CreateAsync(CreateBookDto createBookDto);
        Task UpdateAsync(UpdateBookDto updateBookDto);
        Task DeleteAsync(int id);
        Task DecreaseQauantityAsync(int id);
        Task IncreaseQauantityAsync(int id);
    }
}