namespace LibraryManagementSystem.Dtos.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ModifiedUserId { get; set; }
    }
}