namespace LibraryManagementApi.Models
{
    public class Book
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string AuthorName { get; internal set; }
        public virtual Author Author { get; internal set; }
    }
}