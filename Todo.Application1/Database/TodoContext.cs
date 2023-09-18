using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Models;

namespace Todo.Application.Database
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {
              
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
        }
    }
}
