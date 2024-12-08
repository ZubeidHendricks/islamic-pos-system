namespace IslamicPOS.Server.Middleware;

public class CachingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingMiddleware> _logger;

    public CachingMiddleware(
        RequestDelegate next,
        IMemoryCache cache,
        ILogger<CachingMiddleware> logger)
    {
        _next = next;
        _cache = cache;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;
        var isCacheable = IsCacheablePath(path);

        if (!isCacheable)
        {
            await _next(context);
            return;
        }

        var cacheKey = GenerateCacheKey(context.Request);
        if (_cache.TryGetValue(cacheKey, out byte[] cachedResponse))
        {
            await SendCachedResponse(context, cachedResponse);
            return;
        }

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        if (context.Response.StatusCode == 200)
        {
            var response = await FormatResponse(context.Response);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, response, cacheEntryOptions);
        }

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private bool IsCacheablePath(PathString path)
    {
        return path.StartsWithSegments("/api/products") ||
               path.StartsWithSegments("/api/categories");
    }

    private string GenerateCacheKey(HttpRequest request)
    {
        var keyBuilder = new StringBuilder();
        keyBuilder.Append($"{request.Path}");
        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($"|{key}-{value}");
        }
        return keyBuilder.ToString();
    }

    private async Task SendCachedResponse(
        HttpContext context, 
        byte[] cachedResponse)
    {
        context.Response.ContentType = "application/json";
        await context.Response.Body.WriteAsync(cachedResponse);
    }

    private async Task<byte[]> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var content = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return Encoding.UTF8.GetBytes(content);
    }
}