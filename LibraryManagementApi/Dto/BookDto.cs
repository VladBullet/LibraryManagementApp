namespace LibraryManagementApi.Dto
{
    public class BookDto
    {
        public string Title { get; internal set; }
        public string AuthorName { get; internal set; }
        public bool IsAvailable { get { return Stock > 0; } internal set { IsAvailable = value; } }
        public int Stock { get; internal set; }


    }
}