using PlanogramPhotoApp.Models;

namespace PlanogramPhotoApp.Services;

/// <summary>
/// In-memory store for segment photos. Key: (PlanogramId, SegmentNumber) -> base64 image data.
/// </summary>
public class PhotoStoreService
{
    private readonly Dictionary<(string PlanogramId, int SegmentNumber), string> _photos = new();

    public void StorePhoto(string planogramId, int segmentNumber, string base64ImageData)
    {
        _photos[(planogramId, segmentNumber)] = base64ImageData;
    }

    public string? GetPhoto(string planogramId, int segmentNumber)
    {
        return _photos.TryGetValue((planogramId, segmentNumber), out var data) ? data : null;
    }

    public IReadOnlyDictionary<(string PlanogramId, int SegmentNumber), string> GetAllPhotos() => _photos;

    public IEnumerable<int> GetSegmentNumbersWithPhotos(string planogramId)
    {
        return _photos.Keys
            .Where(k => k.PlanogramId == planogramId)
            .Select(k => k.SegmentNumber)
            .ToList();
    }

    public void ClearPlanogram(string planogramId)
    {
        var keys = _photos.Keys.Where(k => k.PlanogramId == planogramId).ToList();
        foreach (var key in keys)
            _photos.Remove(key);
    }
}
