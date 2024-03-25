using Microsoft.EntityFrameworkCore;

namespace Books_MVC_Project.Models
{
    public class Context:DbContext
    {
        public Context() { }
        public Context(DbContextOptions options):base(options){}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =.;Initial Catalog = Books;Integrated Security = True;");
        }
    }
}
