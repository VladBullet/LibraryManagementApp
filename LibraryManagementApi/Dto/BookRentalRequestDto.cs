namespace LibraryManagementApi.Dto
{
    public class BookRentalRequestDto
    {
        public int UserId { get; set; }
        public IEnumerable<int> BookIds { get; set; }
    }

}
