using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string AuthorName { get; internal set; }
        public virtual Author Author { get; internal set; }
    }
}