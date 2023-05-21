using AspNetCoreHero.ToastNotification.Abstractions;
using LibraryManagementSystem.Dtos.StudentDto;
using LibraryManagementSystem.Helpers.Interface;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interface;
using LibraryManagementSystem.Services.Interface;
using LibraryManagementSystem.ViewModels.StudentViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;
        private readonly INotyfService _notyfService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileHelper _fileHelper;

        public StudentController(IStudentRepository studentRepository, IStudentService studentService, INotyfService notyfService, UserManager<ApplicationUser> userManager, IFileHelper fileHelper)
        {
            _studentRepository = studentRepository;
            _studentService = studentService;
            _notyfService = notyfService;
            _userManager = userManager;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetStudentsWithUserAsync();
            var vm = students.Select(x => new StudentVm()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                ImageUrl = x.ImageUrl,
                Grade = x.Grade
            }).ToList();
            return View(vm);
        }
        public IActionResult Create()
        {
            return View(new CreateStudentVm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var dto = new CreateStudentDto()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    PhoneNumber = vm.PhoneNumber,
                    Email = vm.Email,
                    CreatedUserId = currentUser!.Id,
                    Grade = vm.Grade,
                };
                if (vm.Image != null)
                {
                    dto.ImageUrl = await _fileHelper.UploadFile(vm.Image, "student");
                }
                await _studentService.CreateAsync(dto);
                _notyfService.Success("Student created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentWithUserAsync(id);
                var vm = new StudentDetailsVm()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    CreatedDate = student.CreatedDate,
                    ModifiedDate = student.ModifiedDate,
                    CreatedBy = student.CreatedUser!.FullName,
                    ModifiedBy = student.ModifiedUser!.FullName,
                    ImageUrl = student.ImageUrl,
                    Grade = student.Grade
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentWithUserAsync(id);
                var vm = new EditStudentVm()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    Grade = student.Grade
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
        public async Task<IActionResult> Edit(EditStudentVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var dto = new UpdateStudentDto()
                {
                    Id = vm.Id,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    PhoneNumber = vm.PhoneNumber,
                    Email = vm.Email,
                    Grade = vm.Grade,
                    ModifiedUserId = currentUser!.Id
                };
                if (vm.Image != null)
                {
                    dto.ImageUrl = await _fileHelper.UploadFile(vm.Image, "student");
                }
                await _studentService.UpdateAsync(dto);
                _notyfService.Success("Student updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DeleteAsync(id);
                _notyfService.Success("Student deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}