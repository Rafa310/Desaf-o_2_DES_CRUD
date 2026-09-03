namespace EventosApi.BL.Caching
{
    // Mismo patron de cache-aside con Redis que la Guia #8 (TryGetCacheAsync / TrySetCacheAsync /
    // TryDeleteCacheAsync usando IConnectionMultiplexer), reutilizado aqui en un solo lugar para
    // no repetirlo en cada servicio (Evento, Participante, Organizador).
    public interface ICacheService
    {
        public Task<T?> TryGetCacheAsync<T>(string key);
        public Task TrySetCacheAsync<T>(string key, T value, TimeSpan duration);
        public Task TryDeleteCacheAsync(string key);
    }
}
