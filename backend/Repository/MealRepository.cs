using Backend.Database;
using Backend.Entity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

class MealRepository : GenericEntityDbRepository<Meal>
{
    public MealRepository(MealPlannerContext context, DbSet<Meal> set) : base(context, set)
    {
    }

    public override Meal Create(Meal data)
    {
        var existingRecipe = this.m_context.Recipes.Find(data.Recipe.Id);
        data.Recipe = existingRecipe;   
        return base.Create(data);
    }
}