namespace backend.Dto;

public record MealDto
{
    public long Id { get; set; }
    public DateTime At { get; set; }
    public RecipeDto Recipe { get; set; }
}