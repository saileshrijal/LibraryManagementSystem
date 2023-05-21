using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<List<Student>> GetStudentsWithUserAsync();
        Task<Student> GetStudentWithUserAsync(int id);
    }
}