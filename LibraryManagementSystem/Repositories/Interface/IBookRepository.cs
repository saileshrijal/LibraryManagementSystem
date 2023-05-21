using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories.Interface
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetAllBooksWithUserAndCategory();
        Task<Book> GetBookWithUserAndCategory(int id);
        Task<int> GetTotalBookCount();
        Task<int> GetTotalAvailableBookCount();
    }
}