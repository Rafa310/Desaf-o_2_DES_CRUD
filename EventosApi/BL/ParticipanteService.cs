using AutoMapper;
using EventosApi.BL.Caching;
using EventosApi.BL.Interfaces;
using EventosApi.DAL.Interfaces;
using EventosApi.DTO;
using EventosApi.Models;

namespace EventosApi.BL
{
    public class ParticipanteService(IParticipanteRepository repository, IMapper mapper, ICacheService cache) : IParticipanteService
    {
        private static readonly TimeSpan CacheDuration = TimeSpan.FromSeconds(30);
        private const string CacheKeyAll = "participantes:all";
        private static string CacheKeyById(int id) => $"participantes:{id}";

        public async Task<List<ParticipanteDto>> GetParticipantesAsync()
        {
            var cached = await cache.TryGetCacheAsync<List<ParticipanteDto>>(CacheKeyAll);
            if (cached != null)
            {
                return cached;
            }

            var participantes = await repository.GetParticipantesAsync();
            var result = mapper.Map<List<ParticipanteDto>>(participantes);
            await cache.TrySetCacheAsync(CacheKeyAll, result, CacheDuration);
            return result;
        }

        public async Task<ParticipanteDto?> GetParticipanteByIdAsync(int id)
        {
            var key = CacheKeyById(id);
            var cached = await cache.TryGetCacheAsync<ParticipanteDto>(key);
            if (cached != null)
            {
                return cached;
            }

            var participante = await repository.GetParticipanteByIdAsync(id);
            if (participante == null)
            {
                return null;
            }

            var result = mapper.Map<ParticipanteDto>(participante);
            await cache.TrySetCacheAsync(key, result, CacheDuration);
            return result;
        }

        public async Task<ParticipanteDto> InsertParticipanteAsync(ParticipanteDto participante)
        {
            var entity = mapper.Map<Participante>(participante);
            var newId = await repository.InsertParticipanteAsync(entity);
            participante.Id = newId;
            await cache.TryDeleteCacheAsync(CacheKeyAll);
            return participante;
        }

        public async Task<ParticipanteDto?> UpdateParticipanteAsync(int id, ParticipanteDto participante)
        {
            var entity = mapper.Map<Participante>(participante);
            entity.IdParticipante = id;
            var updated = await repository.UpdateParticipanteAsync(entity);
            if (!updated)
            {
                return null;
            }

            participante.Id = id;
            await cache.TryDeleteCacheAsync(CacheKeyAll);
            await cache.TryDeleteCacheAsync(CacheKeyById(id));
            return participante;
        }

        public async Task<bool> DeleteParticipanteAsync(int id)
        {
            var deleted = await repository.DeleteParticipanteAsync(id);
            if (deleted)
            {
                await cache.TryDeleteCacheAsync(CacheKeyAll);
                await cache.TryDeleteCacheAsync(CacheKeyById(id));
            }
            return deleted;
        }
    }
}
