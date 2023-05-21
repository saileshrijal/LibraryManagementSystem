using LibraryManagementSystem.Dtos.CategoryDto;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interface;
using LibraryManagementSystem.Services.Interface;

namespace LibraryManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUserId = createCategoryDto.CreatedUserId,
                ModifiedUserId = createCategoryDto.CreatedUserId
            };
            await _unitOfWork.CreateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByAsync(x=>x.Id == id);
            await _unitOfWork.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetByAsync(x => x.Id == updateCategoryDto.Id);
            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;
            category.ModifiedDate = DateTime.UtcNow;
            category.ModifiedUserId = updateCategoryDto.ModifiedUserId;
            await _unitOfWork.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }
    }
}