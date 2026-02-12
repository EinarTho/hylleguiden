namespace PlanogramPhotoApp.Models;

public class Planogram
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public int NumberOfSegments { get; set; }
}
