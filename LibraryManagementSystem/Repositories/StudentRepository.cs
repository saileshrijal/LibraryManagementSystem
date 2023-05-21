using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Student>> GetStudentsWithUserAsync()
        {
            return await _context.Students!.Include(x => x.CreatedUser).Include(x => x.ModifiedUser).ToListAsync();
        }

        public async Task<Student> GetStudentWithUserAsync(int id)
        {
            return await _context.Students!.Include(x => x.CreatedUser).Include(x => x.ModifiedUser).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Student not found");
        }
    }
}