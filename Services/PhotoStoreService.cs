using PlanogramPhotoApp.Models;

namespace PlanogramPhotoApp.Services;

/// <summary>
/// In-memory store for segment photos. Key: (PlanogramId, SegmentId) -> base64 image data.
/// </summary>
public class PhotoStoreService
{
    private readonly Dictionary<(string PlanogramId, string SegmentId), string> _photos = new();

    public void StorePhoto(string planogramId, string segmentId, string base64ImageData)
    {
        _photos[(planogramId, segmentId)] = base64ImageData;
    }

    public string? GetPhoto(string planogramId, string segmentId)
    {
        return _photos.TryGetValue((planogramId, segmentId), out var data) ? data : null;
    }

    public IReadOnlyDictionary<(string PlanogramId, string SegmentId), string> GetAllPhotos() => _photos;

    public IEnumerable<string> GetSegmentIdsWithPhotos(string planogramId)
    {
        return _photos.Keys
            .Where(k => k.PlanogramId == planogramId)
            .Select(k => k.SegmentId)
            .ToList();
    }

    public void ClearPlanogram(string planogramId)
    {
        var keys = _photos.Keys.Where(k => k.PlanogramId == planogramId).ToList();
        foreach (var key in keys)
            _photos.Remove(key);
    }
}
