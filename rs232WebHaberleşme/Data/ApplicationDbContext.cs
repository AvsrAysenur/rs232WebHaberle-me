using Microsoft.EntityFrameworkCore;
using rs232WebHaberleşme.Models;
namespace rs232WebHaberleşme.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; } = null!;
    }
}
