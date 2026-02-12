using PlanogramPhotoApp.Models;

namespace PlanogramPhotoApp.Services;

/// <summary>
/// Provides planogram data. In-memory sample data for now.
/// </summary>
public class PlanogramDataService
{
    private readonly List<Planogram> _planograms = new();

    public PlanogramDataService()
    {
        SeedSamplePlanograms();
    }

    private void SeedSamplePlanograms()
    {
        _planograms.Add(new Planogram
        {
            Id = "p1",
            Name = "Beverage Cooler",
            NumberOfSegments = 3
        });
        _planograms.Add(new Planogram
        {
            Id = "p2",
            Name = "Snack Aisle",
            NumberOfSegments = 3
        });
    }

    public IReadOnlyList<Planogram> GetAllPlanograms() => _planograms;

    public Planogram? GetPlanogram(string id) => _planograms.FirstOrDefault(p => p.Id == id);
}
