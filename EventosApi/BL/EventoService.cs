using AutoMapper;
using EventosApi.BL.Caching;
using EventosApi.BL.Interfaces;
using EventosApi.DAL.Interfaces;
using EventosApi.DTO;
using EventosApi.Models;

namespace EventosApi.BL
{
    public class EventoService(IEventoRepository repository, IMapper mapper, ICacheService cache) : IEventoService
    {
        private static readonly TimeSpan CacheDuration = TimeSpan.FromSeconds(30);
        private const string CacheKeyAll = "eventos:all";
        private static string CacheKeyById(int id) => $"eventos:{id}";

        public async Task<List<EventoDto>> GetEventosAsync()
        {
            var cached = await cache.TryGetCacheAsync<List<EventoDto>>(CacheKeyAll);
            if (cached != null)
            {
                return cached;
            }

            var eventos = await repository.GetEventosAsync();
            var result = mapper.Map<List<EventoDto>>(eventos);
            await cache.TrySetCacheAsync(CacheKeyAll, result, CacheDuration);
            return result;
        }

        public async Task<EventoDto?> GetEventoByIdAsync(int id)
        {
            var key = CacheKeyById(id);
            var cached = await cache.TryGetCacheAsync<EventoDto>(key);
            if (cached != null)
            {
                return cached;
            }

            var evento = await repository.GetEventoByIdAsync(id);
            if (evento == null)
            {
                return null;
            }

            var result = mapper.Map<EventoDto>(evento);
            await cache.TrySetCacheAsync(key, result, CacheDuration);
            return result;
        }

        public async Task<EventoDto> InsertEventoAsync(EventoDto evento)
        {
            var entity = mapper.Map<Evento>(evento);
            var newId = await repository.InsertEventoAsync(entity);
            evento.Id = newId;
            await cache.TryDeleteCacheAsync(CacheKeyAll);
            return evento;
        }

        public async Task<EventoDto?> UpdateEventoAsync(int id, EventoDto evento)
        {
            var entity = mapper.Map<Evento>(evento);
            entity.IdEvento = id;
            var updated = await repository.UpdateEventoAsync(entity);
            if (!updated)
            {
                return null;
            }

            evento.Id = id;
            await cache.TryDeleteCacheAsync(CacheKeyAll);
            await cache.TryDeleteCacheAsync(CacheKeyById(id));
            return evento;
        }

        public async Task<bool> DeleteEventoAsync(int id)
        {
            var deleted = await repository.DeleteEventoAsync(id);
            if (deleted)
            {
                await cache.TryDeleteCacheAsync(CacheKeyAll);
                await cache.TryDeleteCacheAsync(CacheKeyById(id));
            }
            return deleted;
        }
    }
}
