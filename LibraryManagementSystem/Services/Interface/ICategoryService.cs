
using LibraryManagementSystem.Dtos.CategoryDto;

namespace LibraryManagementSystem.Services.Interface
{
    public interface ICategoryService
    {
        Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
    }
}