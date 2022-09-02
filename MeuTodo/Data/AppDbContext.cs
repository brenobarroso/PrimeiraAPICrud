using MeuTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }

}