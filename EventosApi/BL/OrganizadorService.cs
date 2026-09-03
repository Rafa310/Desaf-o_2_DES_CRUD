using AutoMapper;
using EventosApi.BL.Caching;
using EventosApi.BL.Interfaces;
using EventosApi.DAL.Interfaces;
using EventosApi.DTO;
using EventosApi.Models;

namespace EventosApi.BL
{
    public class OrganizadorService(IOrganizadorRepository repository, IMapper mapper, ICacheService cache) : IOrganizadorService
    {
        private static readonly TimeSpan CacheDuration = TimeSpan.FromSeconds(30);
        private const string CacheKeyAll = "organizadores:all";
        private static string CacheKeyById(int id) => $"organizadores:{id}";

        public async Task<List<OrganizadorDto>> GetOrganizadoresAsync()
        {
            var cached = await cache.TryGetCacheAsync<List<OrganizadorDto>>(CacheKeyAll);
            if (cached != null)
            {
                return cached;
            }

            var organizadores = await repository.GetOrganizadoresAsync();
            var result = mapper.Map<List<OrganizadorDto>>(organizadores);
            await cache.TrySetCacheAsync(CacheKeyAll, result, CacheDuration);
            return result;
        }

        public async Task<OrganizadorDto?> GetOrganizadorByIdAsync(int id)
        {
            var key = CacheKeyById(id);
            var cached = await cache.TryGetCacheAsync<OrganizadorDto>(key);
            if (cached != null)
            {
                return cached;
            }

            var organizador = await repository.GetOrganizadorByIdAsync(id);
            if (organizador == null)
            {
                return null;
            }

            var result = mapper.Map<OrganizadorDto>(organizador);
            await cache.TrySetCacheAsync(key, result, CacheDuration);
            return result;
        }

        public async Task<OrganizadorDto> InsertOrganizadorAsync(OrganizadorDto organizador)
        {
            var entity = mapper.Map<Organizador>(organizador);
            var newId = await repository.InsertOrganizadorAsync(entity);
            organizador.Id = newId;
            await cache.TryDeleteCacheAsync(CacheKeyAll);
            return organizador;
        }

        public async Task<OrganizadorDto?> UpdateOrganizadorAsync(int id, OrganizadorDto organizador)
        {
            var entity = mapper.Map<Organizador>(organizador);
            entity.IdOrganizador = id;
            var updated = await repository.UpdateOrganizadorAsync(entity);
            if (!updated)
            {
                return null;
            }

            organizador.Id = id;
            await cache.TryDeleteCacheAsync(CacheKeyAll);
            await cache.TryDeleteCacheAsync(CacheKeyById(id));
            return organizador;
        }

        public async Task<bool> DeleteOrganizadorAsync(int id)
        {
            var deleted = await repository.DeleteOrganizadorAsync(id);
            if (deleted)
            {
                await cache.TryDeleteCacheAsync(CacheKeyAll);
                await cache.TryDeleteCacheAsync(CacheKeyById(id));
            }
            return deleted;
        }
    }
}
