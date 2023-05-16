using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LibraryManagementApi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; internal set; }
        [DefaultValue(false)]
        public bool Lent { get; internal set; }
        public virtual Author Author { get; internal set; }
    }
}