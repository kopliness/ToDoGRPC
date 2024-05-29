using Microsoft.EntityFrameworkCore;
using ToDoGRPC.Models;

namespace ToDoGRPC.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
}