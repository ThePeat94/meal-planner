namespace Backend.Entity;

public record Recipe : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}