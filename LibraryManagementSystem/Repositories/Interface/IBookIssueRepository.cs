using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories.Interface
{
    public interface IBookIssueRepository : IRepository<BookIssue>
    {
        Task<List<BookIssue>> GetAllBookIssuesWithProperties();
        Task<List<BookIssue>> GetAllBookIssuesWithPropertiesNotReturned();
        Task<List<BookIssue>> GetAllBookIssuesWithPropertiesReturned();
        Task<BookIssue> GetBookIssueWithProperties(int id);
        Task<int> GetTotalBookIssueCount();
        Task<int> GetTotalBookReturnCount();
    }
}