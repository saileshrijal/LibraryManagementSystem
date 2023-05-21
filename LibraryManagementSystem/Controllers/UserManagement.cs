using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using LibraryManagementSystem.Constants;
using LibraryManagementSystem.Helpers.Interface;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class UserManagement : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;
        public UserManagement(UserManager<ApplicationUser> userManager,
                            INotyfService notyfService,
                            IFileHelper fileHelper)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetUsersInRoleAsync(UserRoles.User);
            var vm = users.Select(x => new UserVm()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImageUrl = x.ImageUrl,
                EmployeeId = x.EmployeeId,
                Status = x.Status,
            }).ToList();
            return View(vm);
        }

        public IActionResult Create()
        {
            return View(new CreateUserVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var checkEmail = await _userManager.FindByEmailAsync(vm.Email!);
                if (checkEmail != null)
                {
                    _notyfService.Error("Email already exists");
                    return View(vm);
                }
                var checkUserName = await _userManager.FindByNameAsync(vm.UserName!);
                if (checkUserName != null)
                {
                    _notyfService.Error("Username already exists");
                    return View(vm);
                }
                var user = new ApplicationUser()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Email = vm.Email,
                    UserName = vm.UserName,
                    PhoneNumber = vm.PhoneNumber,
                    Address = vm.Address,
                    EmployeeId = "EMP-" + DateTime.Now.ToString("ddMMyyyyHHmmss"),
                    Status = true,
                    CreatedDate = DateTime.UtcNow,
                };

                if (vm.Image != null)
                {
                    user.ImageUrl = await _fileHelper.UploadFile(vm.Image, "user");
                }
                var result = await _userManager.CreateAsync(user, vm.Password!);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
                tx.Complete();
                _notyfService.Success("User created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction(nameof(Index));
            }
            var vm = new EditUserVm()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.FindByIdAsync(vm.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.PhoneNumber = vm.PhoneNumber;
                user.Address = vm.Address;
                if (vm.Image != null)
                {
                    user.ImageUrl = await _fileHelper.UploadFile(vm.Image, "user");
                }
                await _userManager.UpdateAsync(user);
                _notyfService.Success("User updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction(nameof(Index));
            }
            var vm = new UserDetailsVm()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ImageUrl = user.ImageUrl,
                CreatedDate = user.CreatedDate,
                EmployeeId = user.EmployeeId,
                Status = user.Status,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                await _userManager.DeleteAsync(user);
                _notyfService.Success("User deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                user.Status = !user.Status;
                await _userManager.UpdateAsync(user);
                _notyfService.Success("User status updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction(nameof(Index));
            }
            var vm = new ResetPasswordVm()
            {
                Id = user.Id,
                FullName = user.FullName,
                EmployeeId = user.EmployeeId,
                UserName = user.UserName,
                Email = user.Email,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.FindByIdAsync(vm.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, vm.Password!);
                _notyfService.Success("Password reset successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }
    }
}