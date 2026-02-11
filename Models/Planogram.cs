namespace PlanogramPhotoApp.Models;

public class Planogram
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public List<Segment> Segments { get; set; } = new();
}
