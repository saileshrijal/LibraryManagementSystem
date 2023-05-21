using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Constants
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static readonly List<string> Value = new List<string>()
        {
            Admin,
            User
        };

        public static SelectList GetSelectLists(string selectedValue)
        {
            return new SelectList(Value, selectedValue);
        }

    }
}