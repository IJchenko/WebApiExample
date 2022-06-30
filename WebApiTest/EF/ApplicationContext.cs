using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.EF
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Journal>? Journals { get; set; }
    }
}
