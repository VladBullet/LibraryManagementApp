namespace LibraryManagementApi.Data.Models
{
    public class BookRental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
