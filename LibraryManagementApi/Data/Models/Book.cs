using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagementApi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; internal set; }
        public string Title { get; internal set; }
        public int Stock { get; internal set; }
        public int AuthorId { get; internal set; }
        public virtual Author Author { get; internal set; }
    }
}