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
            Segments = new List<Segment>
            {
                new() { Id = "s1", Name = "Top shelf", Order = 1 },
                new() { Id = "s2", Name = "Middle shelf", Order = 2 },
                new() { Id = "s3", Name = "Bottom shelf", Order = 3 }
            }.OrderBy(s => s.Order).ToList()
        });
        _planograms.Add(new Planogram
        {
            Id = "p2",
            Name = "Snack Aisle",
            Segments = new List<Segment>
            {
                new() { Id = "s4", Name = "Left section", Order = 1 },
                new() { Id = "s5", Name = "Center section", Order = 2 },
                new() { Id = "s6", Name = "Right section", Order = 3 }
            }.OrderBy(s => s.Order).ToList()
        });
    }

    public IReadOnlyList<Planogram> GetAllPlanograms() => _planograms;

    public Planogram? GetPlanogram(string id) => _planograms.FirstOrDefault(p => p.Id == id);
}
