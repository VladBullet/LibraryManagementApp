using LibraryManagementApi.Models;

namespace LibraryManagementApi.Dto
{
    public class BookDto
    {
        public string Title { get; internal set; }
        public string AuthorName { get; set; }
        public bool IsAvailable { get { return Stock > 0; } }
        public int Stock { get; internal set; }


    }
}