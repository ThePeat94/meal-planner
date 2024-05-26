using backend.Entity;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class MealPlannerContext : DbContext
{
    public string DbPath { get; }
    
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    public MealPlannerContext()
    {
        var folder = Directory.GetCurrentDirectory();
        this.DbPath = Path.Join(folder, "mealplanner.db");
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}