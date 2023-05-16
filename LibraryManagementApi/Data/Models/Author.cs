using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementApi.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; internal set; }
        public string Firstname { get; internal set; }
        public string Lastname { get; internal set; }
        public string Name { get { return Firstname + " " + Lastname; } }
        public virtual List<Book> Books { get; internal set; }
    }
}