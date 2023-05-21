using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using LibraryManagementSystem.Helpers.Interface;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public ProfileController(UserManager<ApplicationUser> userManager, INotyfService notyfService, IFileHelper fileHelper)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction("Index", "Home");
            }
            var vm = new ProfileVm
            {
                EmployeeId = user.EmployeeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction("Index", "Home");
            }
            var vm = new ProfileVm
            {
                EmployeeId = user.EmployeeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction("Index", "Home");
                }
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Address = vm.Address;

                if (vm.Image != null)
                {
                    user.ImageUrl = await _fileHelper.UploadFile(vm.Image, "user");
                }

                await _userManager.UpdateAsync(user);
                _notyfService.Success("Profile updated successfully");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _notyfService.Error("User not found");
                return RedirectToAction("Index", "Home");
            }
            var vm = new ChangePasswordVm
            {
                EmployeeId = user.EmployeeId,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction("Index", "Home");
                }
                var verifyPassword = await _userManager.CheckPasswordAsync(user, vm.OldPassword!);
                if (!verifyPassword)
                {
                    _notyfService.Error("Old password is incorrect");
                    return View(vm);
                }
                await _userManager.ChangePasswordAsync(user, vm.OldPassword!, vm.NewPassword!);
                _notyfService.Success("Password changed successfully");
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