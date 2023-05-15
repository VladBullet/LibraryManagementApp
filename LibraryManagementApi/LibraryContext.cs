using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi
{
    public class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
       
    }
}