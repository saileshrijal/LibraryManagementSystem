using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedUserId { get; set; }
        public ApplicationUser? CreatedUser { get; set; }
        public string? ModifiedUserId { get; set; }
        public ApplicationUser? ModifiedUser { get; set; }
        
    }
}