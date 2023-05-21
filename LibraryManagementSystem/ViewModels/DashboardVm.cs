using LibraryManagementSystem.ViewModels.BookIssueViewModel;

namespace LibraryManagementSystem.ViewModels
{
    public class DashboardVm
    {
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int Students { get; set; }
        public int Categories { get; set; }
        public int TotalBooks { get; set; }
        public int IssuedBooks { get; set; }
        public int ReturnedBooks { get; set; }
        public int AvailableBooks { get; set; }

        public List<BookIssueHistoryVm>? BookIssueHistories { get; set; }
    }
}