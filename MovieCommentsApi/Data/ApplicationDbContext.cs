using Microsoft.EntityFrameworkCore;
using MovieCommentsApi.Models;
namespace MovieCommentsApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Comment> Comments { get; set; }
    }
}
