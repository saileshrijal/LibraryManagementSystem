using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllCategoriesWithUser()
        {
            return await _context.Categories!.Include(c => c.CreatedUser).Include(m => m.ModifiedUser).ToListAsync();
        }

        public async Task<Category> GetCategoryWithUser(int id)
        {
            return await _context.Categories!.Include(c => c.CreatedUser).Include(m => m.ModifiedUser).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Category not found");
        }
    }
}