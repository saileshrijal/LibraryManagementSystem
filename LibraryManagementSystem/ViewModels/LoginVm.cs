using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Username is required"), DataType(DataType.Text)]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password),]
        public string? Password { get; set; }
        public bool IsRemember { get; set; }
    }
}