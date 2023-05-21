using AspNetCoreHero.ToastNotification.Abstractions;
using LibraryManagementSystem.Dtos.CategoryDto;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interface;
using LibraryManagementSystem.Services.Interface;
using LibraryManagementSystem.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyfService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public CategoryController(ICategoryService categoryService, INotyfService notyfService, ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            _categoryService = categoryService;
            _notyfService = notyfService;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllCategoriesWithUser();
            var vm = categories.Select(x => new CategoryVm()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCategoryVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVm vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                var dto = new CreateCategoryDto()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    CreatedUserId = _userManager.GetUserId(User)
                };
                await _categoryService.CreateAsync(dto);
                _notyfService.Success("Category created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByAsync(x => x.Id == id);
                if (category == null) { return NotFound(); }
                var vm = new EditCategoryVm()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryVm vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                var dto = new UpdateCategoryDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Description = vm.Description,
                    ModifiedUserId = _userManager.GetUserId(User)
                };
                await _categoryService.UpdateAsync(dto);
                _notyfService.Success("Category updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                _notyfService.Success("Category deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryWithUser(id);
                var vm = new CategoryDetailsVm()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    CreatedDate = category.CreatedDate,
                    ModifiedDate = category.ModifiedDate,
                    CreatedBy = category.CreatedUser!.FullName,
                    ModifiedBy = category.ModifiedUser!.FullName
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}