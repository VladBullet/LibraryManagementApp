namespace LibraryManagementApi.Models
{
    public class Author
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public virtual List<Book> Books { get; internal set; }
    }
}