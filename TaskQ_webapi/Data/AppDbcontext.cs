using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TaskQ_webapi.Model;

namespace TaskQ_webapi.Data
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) 
        { 

        }
        public DbSet<Todolist> Todolist { get; set; }
    }
}