namespace LibraryManagementSystem.Models
{
    public class BookIssue
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public string? Note { get; set; }
        public bool IsReturn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedUserId { get; set; }
        public ApplicationUser? CreatedUser { get; set; }
        public string? ModifiedUserId { get; set; }
        public ApplicationUser? ModifiedUser { get; set; }
    }
}