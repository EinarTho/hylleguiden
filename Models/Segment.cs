namespace PlanogramPhotoApp.Models;

public class Segment
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
}
