namespace Backend.Entity;

public record Meal : IEntity
{
    public long Id { get; set; }
    public DateTime At { get; set; }
    public Recipe Recipe { get; set; }
}