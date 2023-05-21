using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class BookIssueHistoryRepository : Repository<BookIssueHistory>, IBookIssueHistoryRepository
    {
        public BookIssueHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}