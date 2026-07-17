using Microsoft.Extensions.Caching.Distributed;

namespace SchoolERP.Infrastructure.Services;

public sealed class CacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public Task SetAsync(string key, string value, CancellationToken cancellationToken = default)
        => _cache.SetStringAsync(key, value, cancellationToken);

    public async Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
        => await _cache.GetStringAsync(key, cancellationToken);
}
