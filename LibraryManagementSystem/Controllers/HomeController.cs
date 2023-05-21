using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LibraryManagementSystem.Repositories.Interface;
using LibraryManagementSystem.ViewModels;
using LibraryManagementSystem.ViewModels.BookIssueViewModel;

namespace LibraryManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStudentRepository _studentRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IBookIssueRepository _bookIssueRepository;
    private readonly IBookIssueHistoryRepository _bookIssueHistoryRepository;

    public HomeController(UserManager<ApplicationUser> userManager, IStudentRepository studentRepository, ICategoryRepository categoryRepository, IBookRepository bookRepository, IBookIssueRepository bookIssueRepository, IBookIssueHistoryRepository bookIssueHistoryRepository)
    {
        _userManager = userManager;
        _studentRepository = studentRepository;
        _categoryRepository = categoryRepository;
        _bookRepository = bookRepository;
        _bookIssueRepository = bookIssueRepository;
        _bookIssueHistoryRepository = bookIssueHistoryRepository;
    }

    public async Task<IActionResult> Index()
    {
        var bookIssueHistories = await _bookIssueHistoryRepository.GetAllAsync();
        var vm = new DashboardVm()
        {
            ActiveUsers = _userManager.Users.Where(x=>x.Status).Count(),
            InactiveUsers = _userManager.Users.Where(x=>!x.Status).Count(),
            Students = await _studentRepository.CountAsync(),
            Categories = await _categoryRepository.CountAsync(),
            TotalBooks = await _bookRepository.GetTotalBookCount(),
            AvailableBooks = await _bookRepository.GetTotalAvailableBookCount(),
            IssuedBooks = await _bookIssueRepository.GetTotalBookIssueCount(),
            ReturnedBooks = await _bookIssueRepository.GetTotalBookReturnCount()
        };
        var bookIssueHistoriesVm = bookIssueHistories.Select(x=>new BookIssueHistoryVm()
        {
            Id = x.Id,
            BookIssueId = x.BookIssueId,
            Message = x.Message,
            CreatedDate = x.CreatedDate,
        }).ToList();
        vm.BookIssueHistories = bookIssueHistoriesVm.Take(5).ToList();
        return View(vm);
    }
}
